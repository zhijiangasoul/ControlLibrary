using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Server
{
    public class MainServer
    {
        public static bool IsPrintApiRequestLog = true;
        public string CommonGet(string url, object Param = null)
        {
            if (IsPrintApiRequestLog)
            {
                LogServer.Info("接口：【" + url + "】  参数：【" + JsonConvert.SerializeObject(Param) + "】 ");
            }
            return this.DoGet(url);
        }
        public string CommonPost(string url, object Param = null)
        {
            if (IsPrintApiRequestLog)
            {
                LogServer.Info("接口：【" + url + "】  参数：【" + JsonConvert.SerializeObject(Param) + "】 ");
            }
            return this.DoCommonPost(url, Param);
        }
        public string DoGet(string Url)
        {
            string jsonStr = string.Empty;

            using (var was = new WebClientTime(5000))
            {
                try
                {
                    was.Headers.Add("Content-Type", "application/json");
                    byte[] bt = was.DownloadData(Url);
                    jsonStr = Encoding.UTF8.GetString(bt);

                }
                catch (Exception ex)
                {
                    LogServer.Error(ex);
                }
            }
            return jsonStr;
        }
        public string DoCommonPost(string url, Object obj = null)
        {
            string jsonStr = string.Empty;
            using (var was = new WebClientTime(5000))
            {
                try
                {
                    was.Headers.Add("Content-Type", "application/json");
                    string param = obj == null ? "" : JsonConvert.SerializeObject(obj);
                    byte[] sendData = Encoding.UTF8.GetBytes(param);
                    was.Headers.Add("ContentLength", sendData.Length.ToString());

                    byte[] recData = was.UploadData(url, "POST", sendData);
                    jsonStr = Encoding.UTF8.GetString(recData);
                   
                }
                catch (Exception e)
                {
                    LogServer.Error(e);
                   
                }
            }
            return jsonStr;
        }







    }
}

