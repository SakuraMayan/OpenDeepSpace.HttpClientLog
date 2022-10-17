using System;

namespace OpenDeepSpace.HttpClientLog
{
    /// <summary>
    /// HttpClient请求url的日志
    /// </summary>
    public class HttpClientLog
    {
        //请求部分 HttpRequestMessage

        /// <summary>
        /// 请求的内容
        /// </summary>
        public string RequestContent { get; set; }

        /// <summary>
        /// 请求头信息
        /// </summary>
        public string RequestHeaders { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求Options
        /// </summary>
        public string RequestOptions { get; set; }

        /// <summary>
        /// 请求的Uri
        /// </summary>
        public string RequestUri { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本策略
        /// </summary>
        public string VersionPolicy { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// 请求的格式
        /// </summary>
        public string RequestContentType { get; set; }


        //响应部分
        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 响应格式
        /// </summary>
        public string ResponseContentType { get; set; }

        /// <summary>
        /// 响应头
        /// </summary>
        public string ResponseHeaders { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime? ResponseTime { get; set; }


        //请求开始到响应完成持续时间
        /// <summary>
        /// 整个请求过程持续时间 单位毫秒
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// 链路追踪Id
        /// </summary>
        public string CorrelationId { get; set; }
    }
}
