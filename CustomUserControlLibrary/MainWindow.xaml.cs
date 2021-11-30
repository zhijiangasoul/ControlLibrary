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

namespace CustomUserControlLibrary
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<PersonInfoModel> personInfoModels = new List<PersonInfoModel>();
        public delegate void GetMessageDataDelegate(object model);
        public GetMessageDataDelegate GetMessageHandler { get; set; }
        public UncleWebsocketService uncleWebsocketService { get; set; }
        public MainServer mainServer = new MainServer();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            ClearTemp();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitMainMenu();
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView ltem = sender as ListView;
            SubItem item_0 = ltem.SelectedItem as SubItem;
            switch (item_0.Name)
            {
                case "歌词显示":
                    LrcShowControl healthPointControl = new LrcShowControl("28417153");
                    MainStackPanel.Children.Add(healthPointControl);
                    break;
                case "2":
                    break;
                case "3":

                    break;
                case "4":
                    break;
            }

            
        }

        public void InitMainMenu()
        {
            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("歌词显示"));
            menuRegister.Add(new SubItem("2"));
            menuRegister.Add(new SubItem("3"));
            menuRegister.Add(new SubItem("4"));
            var item6 = new ItemMenu("控件列表", menuRegister);
            UserControlMenuItem Fmenu1 = new UserControlMenuItem(item6);
            Fmenu1.ListViewMenu.SelectionChanged += ListViewMenu_SelectionChanged;
            MainMenu.Children.Add(Fmenu1);
        }
        public void initUncleSocket()
        {
            GetMessageHandler = new GetMessageDataDelegate(GetOtherHeartDataCallback);
            uncleWebsocketService = new UncleWebsocketService(GetMessageHandler, "22632424");
        }
        private void GetOtherHeartDataCallback(object model)
        {
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

        private void Load_Lrc(object sender, RoutedEventArgs e)
        {
            //28417153
            string a = mainServer.CommonGet(@"http://music.163.com/api/song/media?id=28417153");



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
    }
}
