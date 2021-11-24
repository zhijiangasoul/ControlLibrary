using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Server
{
    class WebClientTime : WebClient
    {
        /// <summary>  
        /// 过期时间  
        /// </summary>  
        public int Timeout { get; set; }

        public WebClientTime(int timeout)
        {
            this.Timeout = timeout;
        }
        /// <summary>
        /// 重写GetWebRequest,添加WebRequest对象超时时间 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.UnsafeAuthenticatedConnectionSharing = true;
            return request;
        }
    }
}
