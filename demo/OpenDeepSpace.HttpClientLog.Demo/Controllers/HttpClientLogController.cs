using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace OpenDeepSpace.HttpClientLog.Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HttpClientLogController : ControllerBase
    {

        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientLogController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }



        /// <summary>
        /// 测试RestSharp
        /// </summary>
        [HttpGet]
        public void TestRestSharp()
        {

            var httpClient = httpClientFactory.CreateClient("RestClient");

            httpClient.BaseAddress = new Uri("https://www.baidu.com/");
            RestClient client = new RestClient(httpClient);
            RestRequest request = new RestRequest("/s?ie={ie}&wd={wd}")
                .AddUrlSegment("ie", "UTF-8")
                .AddUrlSegment("wd", 7)
                ;
            var resp = client.GetAsync(request);

        }

        [HttpGet]
        public void TestHttpClientLog()
        {
            var baiduClient = httpClientFactory.CreateClient("BaiduClient");
            baiduClient.GetAsync("");
        }
    }
}
