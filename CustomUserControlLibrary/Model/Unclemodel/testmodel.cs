using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.Unclemodel
{
    public class testmodel
    {
            public string cmd { get; set; }
            public Data data { get; set; }

        public class Data
        {
            public string type { get; set; }
            public string uid { get; set; }
            public string uname { get; set; }
            public string uface { get; set; }
            public string gift_id { get; set; }
            public string gift_name { get; set; }
            public string gift_num { get; set; }
            public string price { get; set; }
            public bool paid { get; set; }
            public string msg { get; set; }
            public string fans_medal_level { get; set; }
            public string guard_level { get; set; }
            public string timestamp { get; set; }
            public object anchor_lottery { get; set; }
            public object pk_info { get; set; }
            public Anchor_Info anchor_info { get; set; }
        }

        public class Anchor_Info
        {
            public string uid { get; set; }
            public string uname { get; set; }
            public string uface { get; set; }
        }

    }
}
