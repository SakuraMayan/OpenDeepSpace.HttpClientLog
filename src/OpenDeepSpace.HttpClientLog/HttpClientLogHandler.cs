using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenDeepSpace.HttpClientLog
{
    /// <summary>
    /// 通过HttpClient请求url的记录请求和响应全过程日志处理
    /// </summary>
    public class HttpClientLogHandler : DelegatingHandler
    {

        private readonly HttpClientLogOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public HttpClientLogHandler(IOptions<HttpClientLogOptions> options, IServiceProvider serviceProvider)
        {
            _options = options.Value;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 重写SendAsync 记录请求日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            //是否记录请求日志
            bool IsRecord = true;

            //uri跳过
            if (_options.ExcludeUris != null && request.RequestUri != null && _options.ExcludeUris.ToList().Any(t => request.RequestUri.AbsoluteUri.StartsWith(t)))
                IsRecord = false;
            //host跳过
            if (_options.ExcludeHosts != null && request.RequestUri != null && _options.ExcludeHosts.ToList().Any(t => t == request.RequestUri.Host))
                IsRecord = false;

            //请求方式排除
            if (_options.ExcludeRequestMethods != null && _options.ExcludeRequestMethods.ToList().Any(t => t.Equals(request.Method.Method, StringComparison.OrdinalIgnoreCase)))
                IsRecord = false;

            //请求内容格式排除
            if (_options.ExcludeRequestContentTypes != null && request.Content != null && request.Content.Headers.ContentType!=null && _options.ExcludeRequestContentTypes.ToList().Any(t => t == request.Content.Headers.ContentType.MediaType))
                IsRecord = false;

            //请求log日志
            HttpClientLog httpClientLog = new HttpClientLog();

            httpClientLog.RequestTime = DateTime.Now;
            httpClientLog.RequestUri = request.RequestUri?.ToString();
            httpClientLog.RequestMethod = request.Method.ToString();
            httpClientLog.RequestContent = request.Content == null ? null : Encoding.UTF8.GetString(await request.Content.ReadAsByteArrayAsync());
            httpClientLog.RequestContentType = request.Content?.Headers?.ContentType?.MediaType;
            httpClientLog.RequestHeaders = JsonConvert.SerializeObject(request.Headers);
            //httpClientLog.RequestOptions = JsonConvert.SerializeObject(request.Options);
            httpClientLog.Version = request.Version.ToString();
            //httpClientLog.VersionPolicy = request.VersionPolicy.ToString();

            var result = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();
            httpClientLog.Duration = stopwatch.ElapsedMilliseconds;


            //响应状态排除
            if (_options.ExcludeStatusCodes != null && _options.ExcludeStatusCodes.ToList().Any(t => t == ((int)result.StatusCode).ToString()))
                IsRecord = false;
            //响应格式排除
            if (_options.ExcludeResponseContentTypes != null && result.Content != null && result.Content.Headers.ContentType!=null && _options.ExcludeResponseContentTypes.Any(t => t == result.Content.Headers.ContentType.MediaType))
                IsRecord = false;

            httpClientLog.ResponseTime = DateTime.Now;
            httpClientLog.ResponseHeaders = JsonConvert.SerializeObject(result.Headers);
            httpClientLog.StatusCode = ((int)result.StatusCode).ToString();

            httpClientLog.ResponseContent = Encoding.UTF8.GetString(await result.Content.ReadAsByteArrayAsync());
            httpClientLog.ResponseContentType = result.Content.Headers.ContentType?.MediaType;


            //需要记录日志
            if (IsRecord)
            {
                var _store=_serviceProvider.GetService<IHttpClientLogStore>();

                if(_store!=null)
                    await _store.SaveHttpClientLogAsync(httpClientLog);

            }

            return result;
        }
    }
}
