using CustomUserControlLibrary.Model.Unclemodel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebSocketSharp;
using zlib;

namespace CustomUserControlLibrary.Server
{
   public class UncleWebsocketService
    {
        RoomInfo Barragemodel = new RoomInfo();
        RoomConfig roomconfig = new RoomConfig();
        MainServer mainServer = new MainServer();
        public delegate void GetMessageDataDelegate(object model);
        private MainWindow.GetMessageDataDelegate getMessageHandler;
        public static WebSocket _webSocket;
        public System.Windows.Threading.DispatcherTimer m_Timer1 = new System.Windows.Threading.DispatcherTimer();
        public string Roomid { get; set; }
        public UncleWebsocketService(MainWindow.GetMessageDataDelegate GetMessageHandler,string roomid)
        {
            getMessageHandler = GetMessageHandler;
            Roomid = roomid;
            InitWebSocket();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitWebSocket()
        {
            Barragemodel = JsonConvert.DeserializeObject<RoomInfo>(mainServer.CommonGet(@"https://api.live.bilibili.com/room/v1/Room/room_init?id=" + Roomid));
            roomconfig = JsonConvert.DeserializeObject<RoomConfig>(mainServer.CommonGet(@"https://api.live.bilibili.com/room/v1/Danmu/getConf?room_id=" + Roomid));
          //  AddBarrage("系统初始化", "正在连接弹幕服务器");
            //WebSocket连接地址ws://121.40.165.18:8800 wss://broadcastlv.chat.bilibili.com:2245/sub
            var socketConnectUrl = @"wss://broadcastlv.chat.bilibili.com:2245/sub";
            try
            {
                _webSocket = new WebSocket(socketConnectUrl);
                _webSocket.OnOpen += WebSocketOnOpen;      //WebSocket打开
                _webSocket.OnMessage += WebSocketOnMessage;//WebSocket获取消息
                _webSocket.OnError += WebSocketOnError;    //WebSocket发生错误
                _webSocket.OnClose += WebSocketOnClose;    //WebSocket关闭
                _webSocket.Connect();                      //连接WebSocket服务端
            }
            catch (Exception ex)
            {
                // ShowMessage("WebSocket初始化异常：" + ex.Message, true);
            }
        }
        #region 打开websocket
     
        private void WebSocketOnOpen(object sender, EventArgs e)
        {
            try
            {
                var packetModel = new { roomid = Barragemodel.data.room_id, uid = 0, protover = 2, token = roomconfig.data.token, platform = "test" };
                var playload = JsonConvert.SerializeObject(packetModel);
                //   auth_params param = new auth_params();
                //   param.roomid = Barragemodel.data.room_id;
                //char[] arr = JsonConvert.SerializeObject(param).ToCharArray();
                //    byte[] byteData = StringToBinary(JsonConvert.SerializeObject(param));
                _webSocket.Send(SendSocketDataAsync(7, playload));
              //  AddBarrage("系统初始化", "WebSocket连接成功");
                initTimer();
            }
            catch (Exception exception)
            {

            }
        }
        public void initTimer()
        {
            m_Timer1 = new System.Windows.Threading.DispatcherTimer();
            m_Timer1.Interval = new TimeSpan(0, 0, 0, 30, 0);
            m_Timer1.IsEnabled = true;
            m_Timer1.Tick += M_Timer1_Tick;
            m_Timer1.Start();
        }
        /// <summary>
        /// websocket心跳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_Timer1_Tick(object sender, EventArgs e)
        {
                SendHeartbeatAsync();
            ////     https://api.live.bilibili.com/room/v1/Danmu/getConf?room_id=21564812
            //     BarrageModel Barragemodel = JsonConvert.DeserializeObject<BarrageModel>(DoGet(@"http://api.live.bilibili.com/ajax/msg?roomid="+ ConfigurationManager.AppSettings["房间号"]));
            //     if (Barragemodel.code == 0)
            //     {
            //         List<BarrageModel.Room> BarrageTempList = new List<BarrageModel.Room>();
            //         BarrageTempList = Barragemodel.data.room;
            //         int LastTimeStamp = Barragemodel.data.room[BarrageTempList.Count - 1].check_info.ts;    
            //         foreach (BarrageModel.Room Am in BarrageTempList)
            //         {
            //             if (Am.check_info.ts> TimeStamp)
            //             {
            //                 AddBarrage(Am.nickname+" : "+Am.text);
            //             }
            //         }
            //         TimeStamp = Barragemodel.data.room[BarrageTempList.Count - 1].check_info.ts;
            //         BarrageTempList = new List<BarrageModel.Room>(0);
            //     }
        }
        public void SendHeartbeatAsync()
        {
            //  SendSocketDataAsync(2);
            _webSocket.Send(SendSocketDataAsync(2));
            //   Debug.WriteLine("Message Sent: Heartbeat");
        }

        public byte[] SendSocketDataAsync(int action, string body = "")
        {
            return SendSocketDataAsync(0, 16, 2, action, 1, body);
        }


        public byte[] SendSocketDataAsync(int packetlength, short magic, short ver, int action, int param = 1, string body = "")
        {
            var playload = Encoding.UTF8.GetBytes(body);
            if (packetlength == 0)
            {
                packetlength = playload.Length + 16;
            }
            var buffer = new byte[packetlength];
            using (var ms = new MemoryStream(buffer))
            {


                var b = EndianBitConverter.BigEndian.GetBytes(buffer.Length);

                ms.WriteAsync(b, 0, 4);
                b = EndianBitConverter.BigEndian.GetBytes(magic);
                ms.WriteAsync(b, 0, 2);
                b = EndianBitConverter.BigEndian.GetBytes(ver);
                ms.WriteAsync(b, 0, 2);
                b = EndianBitConverter.BigEndian.GetBytes(action);
                ms.WriteAsync(b, 0, 4);
                b = EndianBitConverter.BigEndian.GetBytes(param);
                ms.WriteAsync(b, 0, 4);
                if (playload.Length > 0)
                {
                    ms.WriteAsync(playload, 0, playload.Length);
                }
                //  NetStream.WriteAsync(buffer, 0, buffer.Length);

                return buffer;
            }
        }
        #endregion

        #region websocket接受消息
        private void WebSocketOnMessage(object sender, MessageEventArgs e)
        {
            string ErrorString = string.Empty;
            try
            {
                string log = e.Data;


                byte[] utf8 = e.RawData;
                byte[] tmp = new byte[utf8.Length - 16];
                Array.Copy(utf8, 16, tmp, 0, utf8.Length - 16);
                if (tmp.Count() < 16)
                {
                    // writeLog("连接认证:" + Encoding.Default.GetString(tmp));
                    return;
                }
                //var stableBuffer = new byte[16];
                //DanmakuProtocol DP = new DanmakuProtocol();
                //DP = DanmakuProtocol.FromBuffer(utf8);
                byte[] buffer = deCompressBytes(tmp);
                string CmdJsonString = Encoding.UTF8.GetString(buffer);

                ErrorString = CmdJsonString;

                CmdJsonString = CmdJsonString.Substring(16, CmdJsonString.Length - 16);



                int count = 0;
                string[] sArray = Regex.Split(CmdJsonString, "cmd", RegexOptions.IgnoreCase);
                List<string> st = new List<string>();
                foreach (string ss in sArray)
                {
                    count++;

                    string targetString = string.Empty;
                    if (ss.Length < 16)
                    {
                        continue;
                    }
                    if (count != sArray.Length)
                    {
                        targetString = ss.Remove(ss.Length - 18);
                    }
                    else
                    {
                        targetString = ss;
                    }
                    st.Add("{\"cmd" + targetString);

                }
                foreach (string target in st)
                {
                    JToken data = JObject.Parse(target);
                    ProcessDanmaku((string)data.SelectToken("cmd"), CmdJsonString);
                }
            }
            catch (Exception exception)
            {
                try
                {
                    LogServer.Error("异常消息" + ErrorString);
                    //  AddBarrage("WebSocket接收到消息异常");
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 处理弹幕消息 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="buffer"></param>
        public void ProcessDanmaku(string action, string buffer)
        {
            switch (action)
            {
                ///进入房间
                case "WELCOME_GUARD": // (OpHeartbeatReply)
                    {
                        //var viewer = EndianBitConverter.BigEndian.ToUInt32(buffer, 0); //观众人数
                        // Console.WriteLine(viewer);
                       // writeLog("收到进入消息:" + buffer);                                         //  ReceivedRoomCount?.Invoke(this, new ReceivedRoomCountArgs() { UserCount = viewer });
                        break;
                    }
                ///弹幕消息
                case "DANMU_MSG"://playerCommand (OpSendMsgReply)
                    {
                        DANMU_MSG Danmu = JsonConvert.DeserializeObject<DANMU_MSG>(buffer);
                        //   BarrageControl Bc = new BarrageControl();
                        string BarrageTxt = Danmu.info[1].ToString();
                        string[] s = Danmu.info[2].ToString().Split('"');
                        string UserName = s[1];
                        if (null != getMessageHandler)
                        {
                            //省略转换
                            getMessageHandler(Danmu); //调用委托函数
                        }
                        break;
                    }
                ///送礼物
                case "SEND_GIFT": // (OpAuthReply)
                    {
                        //var Danmu = JsonConvert.DeserializeObject<Send_GIFT>(buffer);
                        //AddBarrage(Danmu.data.face, Danmu.data.uname + Danmu.data.action + Danmu.data.giftName + "X" + Danmu.data.combo_stay_time);

                        //AddGiftWindows(Danmu.data.uname + Danmu.data.action + Danmu.data.giftName + "X" + Danmu.data.combo_stay_time, StaticParam.SelectGifPath);
                        //writeLog("收到送礼物消息:" + buffer);
                        break;
                    }
                ///舰长进入
                case "ENTRY_EFFECT": // (OpAuthReply)
                    {
                        //var Danmu = JsonConvert.DeserializeObject<ENTRT_EFFECT>(buffer);
                        //AddBarrage(Danmu.data.face, Danmu.data.copy_writing);
                        //writeLog("收到舰长进入消息:" + buffer);
                        break;
                    }
                ///上船
                case "GUARD_BUY": // (OpAuthReply)
                    {
                        //var Danmu = JsonConvert.DeserializeObject<GUARD_BUY>(buffer);
                        //AddBarrage("", Danmu.data.username + "开通了" + Danmu.data.gift_name + Danmu.data.num + "个月");
                        //writeLog("收到上船消息:" + buffer);
                        break;
                    }
                case "COMBO_SEND": // (OpAuthReply)
                    {
                      //  writeLog("收到连续送礼消息:" + buffer);
                        break;
                    }
                case "INTERACT_WORD": // (OpAuthReply)
                    {
                        //INTERACT_WORD word = JsonConvert.DeserializeObject<INTERACT_WORD>(buffer);
                        //AddBarrage("", "欢迎" + word.data.uname + "进入直播间");

                        //writeLog("收到连续送礼消息:" + buffer);
                        break;
                    }


                default:
                    {
                       // writeLog("收到未解析消息:" + buffer);
                        break;
                    }
            }
        }




        private static byte[] deCompressBytes(byte[] sourceByte)
        {

            MemoryStream inputStream = new MemoryStream(sourceByte);
            Stream outputStream = deCompressStream(inputStream);
            byte[] outputBytes = new byte[outputStream.Length];
            outputStream.Position = 0;
            outputStream.Read(outputBytes, 0, outputBytes.Length);
            outputStream.Close();
            inputStream.Close();
            return outputBytes;
        }

        private static Stream deCompressStream(Stream sourceStream)
        {
            MemoryStream outStream = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(outStream);
            CopyStream(sourceStream, outZStream);
            outZStream.finish();
            return outStream;
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
        #endregion

        #region   websocket关闭
        private void WebSocketOnClose(object sender, CloseEventArgs e)
        {
            //  ShowMessage("WebSocket断开连接!", true);
        }
        #endregion

        #region websocket报错
        private void WebSocketOnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
          //  AddBarrage("WebSocket发生错误:");
        }
        #endregion
    }
}
