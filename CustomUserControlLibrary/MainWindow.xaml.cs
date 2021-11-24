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
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            ClearTemp();           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

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
        TestModel testModel = new TestModel();
        TestModel_1 testModel_1 = new TestModel_1();

     //   28417153


        private void Play_Music(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(SongId.Text))
            {
                return;
            }
            MainPanel.Children.Clear();
            System.Threading.Thread.Sleep(2000);
            GC.Collect();
            LrcShowControl lrcShowControl = new LrcShowControl(SongId.Text);
            MainPanel.Children.Add(lrcShowControl);
        }

        private void Load_Lrc(object sender, RoutedEventArgs e)
        {

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
    }
}
