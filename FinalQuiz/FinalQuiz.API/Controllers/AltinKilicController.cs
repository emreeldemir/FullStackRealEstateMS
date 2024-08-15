using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalQuiz.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AltinKilicController : ControllerBase
    {
        private readonly ILogger<AltinKilicController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly object _lock = new object();

        public AltinKilicController(ILogger<AltinKilicController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }


        // Burada "ConcurrentDictionary" kullanmayı düşündüm fakat
        // "ConcurrentDictionary" herhangi bir bellek yönetimi veya süre sonu politikası içermiyor, buna ihtiyacım var mı emin değilim fakat
        // ilk etapta bunu kullanarak yaptım.
        
        // Siz soruyu anlatırken aklıma "Op Sys" dersinden "Racing Condition" konusu geldi, Semaphore konusunu da hatırladım,
        // eş zamanlı erişimleri kontrol etmek için, ama burada "lock" kullandım. Bu sayede "critical section" ları korumuş oldum.
        [HttpGet]
        [Route("RequestCounter")]
        public ActionResult RequestCounter()
        {
            /// Endpoint anahtarını oluştur
            string cacheKey = $"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName}";
            int count;

            lock (_lock)
            {
                if (!_memoryCache.TryGetValue(cacheKey, out count))
                {
                    count = 0;
                }

                count++;
                _memoryCache.Set(cacheKey, count);

                _logger.LogInformation($"Updated cache for {cacheKey}: {count}");
            }

            return Ok(new { Message = $"This endpoint has been called {count} times today." });

            // Bana öyle bir Kod yazın ki
            // Bu endpointin günde kaç kere çağırıldığını saysın.

            // Çağırımı saymak 10 puan
            // Static keyword'ünü kullanmadan saymak 10 puan
            // Eşzamanlı çağırımların davranışını ele almak 10 puan

        }





        // Burada server kapanmamalı, bu sebeple bu endpoint'i çağırdıktan sonra
        // 5 saniyelik bekleme süresinde "RequestCounter" endpoint'ini de çağırarak çalıştığını gözlemledim.
        // Exception'ları array'de tuttum bu sayede yeni bir exception eklemek istediğimde array'e ekleyerek yapabilirim.
        // Bunun yerine switch-case yapısı kullanarak da yapabilirdim fakat array ile yapmak daha esnek olacaktır diye düşündüm.
        [HttpGet]
        [Route("MimicException")]
        public async Task<ActionResult> MimicException()
        {
            await Task.Delay(5000);

            var exceptions = new Exception[]
            {
                new ArgumentNullException("Parameter cannot be null."),
                new IndexOutOfRangeException("Index was out of range."),
                new NullReferenceException("Object reference not set to an instance of an object.")
            };
                     
            var random = new Random();
            var selectedException = exceptions[random.Next(exceptions.Length)];

            throw selectedException;

            // Bana öyle bir Kod yazın ki
            // Sizin servisinizi çağıracak olan bir ekip, 
            // 5 sn bekledikten sonra aşağıdaki 3 exception'dan bir tanesi rastgele olarak alıyor olsun.
            // ArgumentNullException, IndexOutOfRangeException, NullReferenceException

            // Exception dönmek 5 puan
            // Rastgele exception dönmek 5 puan
            // 5sn bekledikten sonra yanıt dönmek 10 puan


        }





        [HttpGet]
        [Route("GetStatistics")]
        public ActionResult GetStatistics()
        {
            var statistics = new Dictionary<string, int>();

            // Dinamik olarak endpoint'leri al
            var endpointData = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => m.GetCustomAttributes(typeof(HttpMethodAttribute), true).Length > 0)
                .Select(m => new { Controller = t.Name, Method = m.Name }))
                .ToList();

            foreach (var endpoint in endpointData)
            {
                string endpointName = $"{endpoint.Controller}/{endpoint.Method}";

                // Cache'den count'u al
                int count = GetCurrentCount(endpointName);

                // İstatistikler sözlüğüne ekle
                statistics[endpointName] = count;

                _logger.LogInformation($"Endpoint: {endpointName}, Count: {count}");
            }

            return Ok(statistics);


        // Bana öyle bir altyapı hazırlayın ki
        // Bu endpoint çağırıldığında yanıt olarak
        // Bu projedeki hangi endpointin kaç kere çağırıldığının istatistiğini tutsun.
        // Örn : RequestCounter:10, MimicException:3 benzeri bir yanıt dönsün.

        // Uygulama çalışmaya başladığından beri ki yanıtı dönmek 15 puan
        // Bulunduğum takvim günü 00:00 dan beri ki istatistiği dönmek +5 puan (öncül işlem tamamlanmalı)
        // Yeni bir endpoint eklediğimde, hiçbir geliştirme yapmadan onu da saymaya başlaması;
        // Doğru yöntemi bulmak 10 puan (görmeyi beklediğim kodlar var, çalışmasada onu görmem yeterli)
        // Doğru yöntemi tamamlamak 20 puan (çalışmasını beklerim)


        }

        [HttpGet]
        [Route("TestCache")]
        public ActionResult TestCache()
        {
            string testKey = "TestKey";
            int testCount = 1;

            _memoryCache.Set(testKey, testCount);
            int retrievedCount = _memoryCache.TryGetValue(testKey, out int count) ? count : 0;

            return Ok(new { TestKey = testKey, StoredCount = testCount, RetrievedCount = retrievedCount });
        }

        private int GetCurrentCount(string endpoint)
        {
            bool exists = _memoryCache.TryGetValue(endpoint, out int count);
            _logger.LogInformation($"Cache key '{endpoint}' exists: {exists}, count: {count}");
            return exists ? count : 0;
        }
    }
}
