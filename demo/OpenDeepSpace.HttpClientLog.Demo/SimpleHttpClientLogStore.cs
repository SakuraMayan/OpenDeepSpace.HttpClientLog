using Newtonsoft.Json;

namespace OpenDeepSpace.HttpClientLog.Demo
{
    /// <summary>
    /// SimpleHttpClientLogStore
    /// </summary>
    public class SimpleHttpClientLogStore : IHttpClientLogStore
    {

        private readonly ILogger<SimpleHttpClientLogStore> _logger;

        public SimpleHttpClientLogStore(ILogger<SimpleHttpClientLogStore> logger)
        {
            _logger = logger;
        }

        public async Task SaveHttpClientLogAsync(HttpClientLog httpClientLog)
        {

            _logger.LogInformation($"{JsonConvert.SerializeObject(httpClientLog)}");

            await Task.CompletedTask;
        }
    }
}
