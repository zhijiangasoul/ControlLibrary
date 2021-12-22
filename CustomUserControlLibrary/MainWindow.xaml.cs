using CustomUserControlLibrary.Model;
using CustomUserControlLibrary.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using CustomUserControlLibrary.Server;
using CustomUserControlLibrary.View;
using System.Collections.ObjectModel;
using CustomUserControlLibrary.Model.TencentModel;
using CustomUserControlLibrary.Model.WYYmodel;
using CustomUserControlLibrary.Model.SougouModel;
using System.Net;

namespace CustomUserControlLibrary
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<object> BarrageList = new ObservableCollection<object>();
        public ObservableCollection<object> SongList = new ObservableCollection<object>();



        public List<PersonInfoModel> personInfoModels = new List<PersonInfoModel>();
        public delegate void GetMessageDataDelegate(object model);
        public GetMessageDataDelegate GetMessageHandler { get; set; }
        public UncleWebsocketService uncleWebsocketService { get; set; }
        public MainServer mainServer = new MainServer();
        public MainWindow()
        {
            InitializeComponent();
          //  SelectPath();
            this.Loaded += MainWindow_Loaded;
         //   ClearTemp();
            //   APItest();

         //   Load_KugouLrc("quiet");



        }
        NeteaseMusicAPI api = new NeteaseMusicAPI();
        //22632424
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BarrageListView.ItemsSource = BarrageList;
            SongListView.ItemsSource = SongList;
            InitMainMenu();
            AddBarrage("弹幕绑定测试");
        }
        public void WyySongSearch(string Songname)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("s", Songname);
            dic.Add("offset", "0");
            dic.Add("limit", "5");
            dic.Add("type", "1");
            SearchSongNameModel searchSongModel = JsonConvert.DeserializeObject<SearchSongNameModel>(api.GetSong(dic));

            foreach(SearchSongNameModel.Song data in searchSongModel.result.songs)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Tag = data;
                textBlock.Text = data.name + "——" + data.artists[0].name + " 来自网易云";
                textBlock.MouseLeftButtonUp += TextBlock_MouseLeftButtonDown;
                textBlock.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                SongList.Add(textBlock);


            }



        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStackPanel.Children.Clear();
            ListView ltem = sender as ListView;
            SubItem item_0 = ltem.SelectedItem as SubItem;
            switch (item_0.Name)
            {
                case "歌词显示":
                   // Load_Lrc(null,null);或者直接加载lrc到本地在传参
                    LrcShowControl lrcShowControl = new LrcShowControl();
                    lrcShowControl.InitSong("1903635166");
                  //  lrcShowControl.InitSong("","local", SelectPath(), SelectPath()); 没测
                    MainStackPanel.Children.Add(lrcShowControl);
                    break;
                case "血条":
                    //HealthPointControl healthPointControl = new HealthPointControl();
                    //MainStackPanel.Children.Add(healthPointControl);
                    break;
                case "投票器":
                    break;
                case "倒计时":
                    CountDownControl countDownControl = new CountDownControl(60,false);
                    MainStackPanel.Children.Add(countDownControl);
                    break;
                case "弹幕上屏":
                    break;
            }
        }


        private string SelectPath()
        {
            string path = string.Empty;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "(*.*)|*.*"//筛选条件 "(*.*)|*.*.mp3|描述写什么都行(*.txt)|*.txt|(*.*)|*.*              
            };
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Title = "选择歌词文件";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;
            }
            else
            {
                return null;
            }
            return path;
        }




            public void AddBarrage(string Result)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                if(BarrageList.Count>=20)
                {
                    BarrageList.RemoveAt(0);
                }
                //   BarrageStyle model = new BarrageStyle();
                //  model.Barrage = Result;
                BarrageList.Add(Result);
          //      BarrageListView.Items.Add(Result);
                //   BarrageListView.SelectedIndex = BarrageListView.Items.Count - 1;
                BarrageListView.ScrollIntoView(BarrageListView.SelectedItem);
            }));
        }
        public void InitMainMenu()
        {
            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("歌词显示"));
            menuRegister.Add(new SubItem("血条"));
            menuRegister.Add(new SubItem("投票器"));
            menuRegister.Add(new SubItem("弹幕上屏"));
            var item6 = new ItemMenu("控件列表", menuRegister);
            UserControlMenuItem Fmenu1 = new UserControlMenuItem(item6);
            Fmenu1.ListViewMenu.SelectionChanged += ListViewMenu_SelectionChanged;
            MainMenu.Children.Add(Fmenu1);
        }
        public void initUncleSocket(string Roomid)
        {
            GetMessageHandler = new GetMessageDataDelegate(GetOtherHeartDataCallback);
            uncleWebsocketService = new UncleWebsocketService(GetMessageHandler, Roomid);
        }
        private void GetOtherHeartDataCallback(object model)
        {        
                AddBarrage(model.ToString());
            //添加到投票器
        }
        //   28417153
        //private void Play_Music(object sender, RoutedEventArgs e)
        //{

        //    if (string.IsNullOrEmpty(SongId.Text))
        //    {
        //        return;
        //    }
        //    MainPanel.Children.Clear();
        //    System.Threading.Thread.Sleep(2000);
        //    GC.Collect();
        //    LrcShowControl lrcShowControl = new LrcShowControl(SongId.Text);
        //    MainPanel.Children.Add(lrcShowControl);
        //}
        public string ReadTxtContent(string Path)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(Path, Encoding.UTF8);
            string content;
            while ((content = sr.ReadLine()) != null)
            {
                return content.ToString();
            }
            return null;
        }
        #region 其他方法
        public void ClearTemp()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "temp";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);//不存在就创建目录 
            }
            else
            {
                try
                {
                    Directory.Delete(filePath, true);
                    Directory.CreateDirectory(filePath);
                }
                catch
                {
                    //存在文件被暂用情况
                }
            }
        }

        #endregion

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border bd = sender as Border;
            switch (bd.Tag.ToString())
            {
                case "1":
                    this.WindowState = WindowState.Minimized;
                    break;
                case "2":
                    break;
                case "3":
                    this.Close();
                    break;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            initUncleSocket(FRoomid.Text);
        }

        private void SongSearch_Click(object sender, RoutedEventArgs e)
        {
            SongList.Clear();
            Load_Lrc(FSongName.Text);
            WyySongSearch(FSongName.Text);
            Load_KugouLrc(FSongName.Text);


        }
        //存档
        //https://www.cnblogs.com/daxiangxm/p/migu_music_api.html
        //https://www.cnblogs.com/daxiangxm/p/kugou_music_api.html

        private void Load_KugouLrc(string SongName)
        {
            string SongInfoJson = mainServer.CommonGet(@"http://msearchcdn.kugou.com/api/v3/search/song?showtype=14&highlight=em&pagesize=5&tag_aggr=1&tagtype=全部&plat=0&sver=5&keyword="+ SongName + @"&correct=1&api_ver=1&version=9108&page=1&area_code=1&tag=1&with_res_tag=1" );
            SongInfoJson = SongInfoJson.Substring(23, SongInfoJson.Length-44);
            SougouSearchModel searchSongModel = JsonConvert.DeserializeObject<SougouSearchModel>(SongInfoJson);



            foreach (SougouSearchModel.Info SongInfo in searchSongModel.data.info)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Tag = SongInfo;
                textBlock.Text = SongName + "——" + SongInfo.singername + " 来自搜狗音乐";
                textBlock.MouseLeftButtonUp += TextBlock_MouseLeftButtonDown;
                textBlock.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                SongList.Add(textBlock);
            }
        }

        private void Load_Lrc(string SongName)
        {
            string SongInfoJson = mainServer.CommonGet(@"https://c.y.qq.com/soso/fcgi-bin/client_search_cp?aggr=1&cr=1&flag_qc=0&p=1&n=5&w=" + SongName);
            SongInfoJson = SongInfoJson.Replace("callback", "");
            SongInfoJson = RemoveLastStr(RemoveFirstStr(SongInfoJson));
            SearchSongModel searchSongModel = JsonConvert.DeserializeObject<SearchSongModel>(SongInfoJson);
            foreach (SearchSongModel.List SongInfo in searchSongModel.data.song.list)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Tag = SongInfo;
                textBlock.Text = SongName + "——" + SongInfo.singer[0].name +" 来自QQ音乐";
                textBlock.MouseLeftButtonUp += TextBlock_MouseLeftButtonDown;
                textBlock.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                SongList.Add(textBlock);
            }
        }



        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            object targetTag = textBlock.Tag;
            string objType =  targetTag.GetType().ToString();
            switch(objType)
            {
                case "":
                    break;
                case " ":
                    break;
                case "  ":
                    break;
            }


            //SougouSearchModel.Info SongInfo = targetTag as SougouSearchModel.Info;
            //GetVkey(SongInfo);
        }


       // 

        public void GetVkey(SougouSearchModel.Info SongMid)
        {
            //byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            //int iRoot = BitConverter.ToInt32(buffer, 0);//利用BitConvert方法把字节数组转换为整数
            //Random rdmNum = new Random(iRoot);
            string SongInfoJson = mainServer.CommonGet(@"http://m.kugou.com/app/i/getSongInfo.php?cmd=playInfo&hash="+SongMid.hash);
            string LrcJson = mainServer.CommonGet(@" http://www.kugou.com/yy/index.php?r=play/getdata&hash=" + SongMid.hash);
            kugouLrcModel kugoulrcModel = JsonConvert.DeserializeObject<kugouLrcModel>(LrcJson);
            //  SongInfoJson = SongInfoJson.Substring(23, SongInfoJson.Length - 44);
            KugouDownModel kugouDownModel = JsonConvert.DeserializeObject<KugouDownModel>(SongInfoJson);
           string  SongPath = AppDomain.CurrentDomain.BaseDirectory + "temp/" + DateTime.Now.ToString("ffffff") + ".mp3";
            DownloadFile(kugouDownModel.backup_url[0], SongPath);
        }

        public bool DownloadFile(string url, string destFilePath)
        {
            try
            {
                //创建目标目录
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(destFilePath)))
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFilePath));
                // 设置参数
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //发送请求并获取相应回应数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(destFilePath, FileMode.Create);
                byte[] buf = new byte[10240];
                int readSize = responseStream.Read(buf, 0, buf.Length);
                while (readSize > 0)
                {
                    stream.Write(buf, 0, readSize);
                    readSize = responseStream.Read(buf, 0, buf.Length);
                }
                stream.Close();
                stream.Dispose();
                responseStream.Close();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }


        public string GetRandomGuid()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            int iRoot = BitConverter.ToInt32(buffer, 0);//利用BitConvert方法把字节数组转换为整数
            Random rdmNum = new Random();
            string str = rdmNum.Next().ToString();
            buffer = null;
            return str;
        }



        public void AddSong(string Result)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                //   BarrageStyle model = new BarrageStyle();
                //  model.Barrage = Result;
                SongList.Add(Result);
                //      BarrageListView.Items.Add(Result);
                //   BarrageListView.SelectedIndex = BarrageListView.Items.Count - 1;
            }));
        }





        public string RemoveFirstStr(string str)
        {
            string targetstr = string.Empty;
            targetstr = str.Substring(1, str.Length - 1);
            return targetstr;
        }

        public string RemoveLastStr(string str)
        {
            string targetstr = string.Empty;
            targetstr = str.Remove(str.Length - 1, 1);
            return targetstr;
        }

        private void FSongName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter&&!string.IsNullOrWhiteSpace(FSongName.Text))
            {
                SongSearch_Click(null,null);
            }
        }
    }
}
