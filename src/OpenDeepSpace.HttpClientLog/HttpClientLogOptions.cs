using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDeepSpace.HttpClientLog
{
    /// <summary>
    /// HttpClientLog选项
    /// </summary>
    public class HttpClientLogOptions
    {

        /// <summary>
        /// 排除请求格式
        /// </summary>
        public List<string> ExcludeRequestContentTypes { get; set; }

        /// <summary>
        /// 排出的响应格式
        /// </summary>
        public List<string> ExcludeResponseContentTypes { get; set; }

        /// <summary>
        /// 排除的Uri
        /// </summary>
        public List<string> ExcludeUris { get; set; }

        /// <summary>
        /// 排除的Host
        /// </summary>
        public List<string> ExcludeHosts { get; set; }

        /// <summary>
        /// 排除的请求方式
        /// </summary>
        public List<string> ExcludeRequestMethods { get; set; }

        /// <summary>
        /// 排除的响应状态
        /// </summary>
        public List<string> ExcludeStatusCodes { get; set; }

        public HttpClientLogOptions()
        {
            ExcludeRequestContentTypes = new List<string>();
            ExcludeResponseContentTypes = new List<string>();
            ExcludeUris = new List<string>();
            ExcludeHosts = new List<string>();
            ExcludeRequestMethods = new List<string>();
            ExcludeStatusCodes = new List<string>();
        }
    }
}
