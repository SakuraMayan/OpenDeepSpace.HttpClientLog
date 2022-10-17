using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.HttpClientLog.Extensions
{
    /// <summary>
    /// HttpClientBuilder拓展
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="builder"></param>
        /// <param name="httpClientLogOptionsAction"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddHttpMessageHandler<THandler>(this IHttpClientBuilder builder, Action<HttpClientLogOptions> httpClientLogOptionsAction = null) where THandler : DelegatingHandler
        {

            //DelegatingHandler不能复用或缓存。所以将注入的生命周期修改为AddScoped
            builder.Services.AddScoped(typeof(THandler));

            //httpClientLogOptions的配置
            if (httpClientLogOptionsAction == null)
                httpClientLogOptionsAction = (httpclientLogOptions) => { };
            builder.Services.Configure(httpClientLogOptionsAction);

            builder.Services.Configure<HttpClientFactoryOptions>(builder.Name, options =>
            {
                options.HttpMessageHandlerBuilderActions.Add(builder =>
                {
                    builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<THandler>());
                });
            });
            return builder;
        }
    }
}
