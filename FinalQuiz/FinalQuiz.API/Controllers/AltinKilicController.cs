using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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


        //Burada ConcurrentDictionary kullanmayı düşündüm fakat
        //ConcurrentDictionary herhangi bir bellek yönetimi veya süre sonu politikası içermiyor. 
        [HttpGet]
        [Route("RequestCounter")]
        public ActionResult RequestCounter()
        {
            string cacheKey = "RequestCounter";
            int count;

            // Thread-safe increment
            lock (_lock)
            {
                if (!_memoryCache.TryGetValue(cacheKey, out count))
                {
                    count = 0;
                }

                count++;
                _memoryCache.Set(cacheKey, count);
            }

            return Ok(new { Message = "This endpoint has been called " + count + " times today." });
        }

        [HttpGet]
        [Route("MimicException")]
        public ActionResult MimicException()
        {
            // Bana öyle bir Kod yazın ki
            // Sizin servisinizi çağıracak olan bir ekip, 
            // 5 sn bekledikten sonra aşağıdaki 3 exception'dan bir tanesi rastgele olarak alıyor olsun.
            // ArgumentNullException, IndexOutOfRangeException, NullReferenceException

            // Exception dönmek 5 puan
            // Rastgele exception dönmek 5 puan
            // 5sn bekledikten sonra yanıt dönmek 10 puan

            return Ok();
        }

        [HttpGet]
        [Route("GetStatistics")]
        public ActionResult GetStatistics()
        {
            // Bana öyle bir altyapı hazırlayın ki
            // Bu endpoint çağırıldığında yanıt olarak
            // Bu projedeki hangi endpointin kaç kere çağırıldığının istatistiğini tutsun.
            // Örn : RequestCounter:10, MimicException:3 benzeri bir yanıt dönsün.

            // Uygulama çalışmaya başladığından beri ki yanıtı dönmek 15 puan
            // Bulunduğum takvim günü 00:00 dan beri ki istatistiği dönmek +5 puan (öncül işlem tamamlanmalı)
            // Yeni bir endpoint eklediğimde, hiçbir geliştirme yapmadan onu da saymaya başlaması;
            //      Doğru yöntemi bulmak 10 puan (görmeyi beklediğim kodlar var, çalışmasada onu görmem yeterli)
            //      Doğru yöntemi tamamlamak 20 puan (çalışmasını beklerim)

            return Ok();
        }
    }
}
