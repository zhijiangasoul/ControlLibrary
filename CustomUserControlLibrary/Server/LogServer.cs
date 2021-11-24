using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Server
{
    public class LogServer
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Error(Exception e)
        {
            try
            {
                if (e == null)
                {
                    return;
                }
                LogEventInfo logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "", "");
                logEventInfo.Properties["MESSAGE"] = e.StackTrace + "<<<<>>>" + e.Message;
                logger.Log(logEventInfo);
            }
            catch { }
        }
        public static void Error(string Msg)
        {
            try
            {
                if (String.IsNullOrEmpty(Msg))
                {
                    return;
                }
                LogEventInfo logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "", "");
                logEventInfo.Properties["MESSAGE"] = Msg;
                logger.Log(logEventInfo);
            }
            catch { }

        }
        public static void Info(string Msg)
        {
            try
            {
                if (String.IsNullOrEmpty(Msg))
                {
                    return;
                }
                LogEventInfo logEventInfo = new LogEventInfo(NLog.LogLevel.Info, "", "");
                logEventInfo.Properties["MESSAGE"] = Msg;
                logger.Log(logEventInfo);
            }
            catch { }

        }

        /// <summary>
        /// 根据传入days决定保留文件数量，如果days=6则保留
        /// </summary>
        /// <param name="days">保留日志天数</param>
        public static void DeleteLogFilesByDay(int days)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory + "logs/";
                DateTime dt = DateTime.Now;
                //1、将文件不需要删除的文件生成到set集合中
                HashSet<string> keepFiles = new HashSet<string>();
                keepFiles.Add(basePath + dt.ToString("yyyy-MM-dd") + ".log");
                for (int i = 0; i < days; i++)
                {
                    dt = dt.AddDays(-1);
                    keepFiles.Add(basePath + dt.ToString("yyyy-MM-dd") + ".log");
                }

                //2、获取所有日志文件生成set集合
                string pattern = "*.log";
                string[] strFileName = Directory.GetFiles(basePath, pattern);
                HashSet<string> allFiles = new HashSet<string>(strFileName);
                //3、取差集
                allFiles.ExceptWith(keepFiles);
                foreach (string item in allFiles)
                {
                    File.Delete(item);
                }
            }
            catch (Exception ex)
            {
                LogServer.Error(ex);
            }
        }


        #region 测试日志数据
        public static void CreateFile()
        {
            string baseurl = AppDomain.CurrentDomain.BaseDirectory + "logs/";
            DateTime first_dt = new DateTime(2017, 10, 1);
            for (int i = 0; i < 50; i++)
            {
                first_dt = first_dt.AddDays(1);
                string filename = first_dt.ToString("yyyy-MM-dd") + ".log";
                FileStream fs = new FileStream(baseurl + filename, FileMode.Create);
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes("Hello World!");
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
        }
        #endregion
    }
}
