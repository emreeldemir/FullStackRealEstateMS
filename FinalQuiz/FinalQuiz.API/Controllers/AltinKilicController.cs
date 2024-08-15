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

        [HttpGet]
        [Route("RequestCounter")]
        public ActionResult RequestCounter()
        {
            string cacheKey = $"{ControllerContext.ActionDescriptor.ActionName}";
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
        }

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
        }

        [HttpGet]
        [Route("GetStatistics")]
        public ActionResult GetStatistics()
        {
            var statistics = new Dictionary<string, int>();

            var endpointData = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => m.GetCustomAttributes(typeof(HttpMethodAttribute), true).Length > 0)
                .Select(m => new { Method = m.Name }))
                .ToList();

            foreach (var endpoint in endpointData)
            {
                string endpointName = $"{endpoint.Method}";

                int count = GetCurrentCount(endpointName);

                statistics[endpointName] = count;

                _logger.LogInformation($"Endpoint: {endpointName}, Count: {count}");
            }

            return Ok(statistics);
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
