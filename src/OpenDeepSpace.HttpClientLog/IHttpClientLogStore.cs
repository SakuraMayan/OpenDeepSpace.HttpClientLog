using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeepSpace.HttpClientLog
{
    /// <summary>
    /// httpclientlog存储接口
    /// </summary>
    public interface IHttpClientLogStore
    {
        /// <summary>
        /// 保存HttpClientLog
        /// </summary>
        /// <param name="httpClientLog"></param>
        /// <returns></returns>
        Task SaveHttpClientLogAsync(HttpClientLog httpClientLog);
    }
}
