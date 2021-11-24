using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.Unclemodel
{
    public class RoomConfig
    {


        public int code { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public float refresh_row_factor { get; set; }
        public int refresh_rate { get; set; }
        public int max_delay { get; set; }
        public int port { get; set; }
        public string host { get; set; }
        public Host_Server_List[] host_server_list { get; set; }
        public Server_List[] server_list { get; set; }
        public string token { get; set; }
    }

    public class Host_Server_List
    {
        public string host { get; set; }
        public int port { get; set; }
        public int wss_port { get; set; }
        public int ws_port { get; set; }
    }

    public class Server_List
    {
        public string host { get; set; }
        public int port { get; set; }
    }
}
