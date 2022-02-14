using CustomUserControlLibrary.Model;
using CustomUserControlLibrary.Model.WYYmodel;
using CustomUserControlLibrary.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace CustomUserControlLibrary.Control
{
    /// <summary>
    /// LrcShowControl.xaml 的交互逻辑
    /// </summary>
    public partial class LrcShowControl : UserControl
    {
        NeteaseMusicAPI api = null;
        DispatcherTimer dt;
        SearchInfo globalSearchInfo = new SearchInfo();
        public string path = AppDomain.CurrentDomain.BaseDirectory;
        public string SongUrl;
        // 输出文件编码
        OUTPUT_ENCODING_ENUM output_encoding_enum;
        // 搜索类型
        SEARCH_TYPE_ENUM search_type_enum;
        // 展示歌词类型
        SHOW_LRC_TYPE_ENUM show_lrc_type_enum;
        // 输出文件名类型
        OUTPUT_FILENAME_TYPE_ENUM output_filename_type_enum;

        public void InitSong(string SongId,string LoadType="", string Lrc = "",string LocalSongPath="")
        {
            ReloadConfig();
            string lrc = "";
            string SongPath = string.Empty;
            api = new NeteaseMusicAPI();
            if(LoadType=="local")
            {
                lrc = Lrc;
                SongPath = LocalSongPath;
            }
            else 
            {
                lrc = SingleSearch(SongId);
                SongPath = path + "temp/" + DateTime.Now.ToString("ffffff") + ".mp3";
                DownloadFile(SongUrl, SongPath);
                System.Threading.Thread.Sleep(2000);
            }
            LogServer.Info(lrc);

            LrcView.LoadLrc(lrc);

            //初始化计时器

            dt = new DispatcherTimer();
            /*
             * 2018年1月12日10:04:48
             * 1秒有些延迟调整为0.5秒
             * 
             * */
            dt.Interval = TimeSpan.FromSeconds(0.5);

            dt.Tick += (e, c) =>
            {
                LrcRoll();
            };
            Play(false);
            me.Source = new Uri(SongPath, UriKind.RelativeOrAbsolute);
            me.Volume = 50;
           
            //设置为pause时才能达到启动应用后马上加载音乐以获取总时长
            me.LoadedBehavior = MediaState.Pause;
            me.LoadedBehavior = MediaState.Manual;
            //加载音乐后获取总时长
            me.MediaOpened += (e, c) =>
            {
                //将总时长转换为秒单位
                sd.Maximum = (me.NaturalDuration.TimeSpan.Minutes * 60) + me.NaturalDuration.TimeSpan.Seconds;
                //重新设置为手动播放模式，否则无法播放音乐
                me.LoadedBehavior = MediaState.Manual;
                double LastNextTime = LrcView.Lrcs.Last().Value.Time;
                LrcView.Lrcs.Last().Value.NextTime = sd.Maximum * 1000 - LastNextTime;
            };

           

            //监听slider的鼠标释放事件，即拖动时不调整音乐的进度，拖动后再调整
            sd.PreviewMouseLeftButtonUp += (e, c) =>
            {
                STime st = GetStime(sd.Value);
                me.Position = new TimeSpan(0, st.m, st.s);

                LrcRoll();

            };
            //对slider的值变化监听，修改时可实时显示调整的时间
            sd.ValueChanged += (e, c) =>
            {
                STime st = GetStime(sd.Value);
                TimeSpan ts = new TimeSpan(0, st.m, st.s);
                metime.Text = ts.ToString("mm") + ":" + ts.ToString("ss");
            };

        }



        public LrcShowControl()
        {
            InitializeComponent();

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


        void LrcRoll()
        {

            metime.Text = me.Position.ToString("mm") + ":" + me.Position.ToString("ss");
            sd.Value= sd.Value+0.5;
            LrcView.LrcRoll(me.Position.TotalMilliseconds);
        }
        void Play(bool s)
        {
            if (s)
            {
                sbtn.IsEnabled = true;
                sbtn.Visibility = Visibility.Visible;
                pbtn.IsEnabled = false;
                pbtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                sbtn.IsEnabled = false;
                sbtn.Visibility = Visibility.Collapsed;
                pbtn.IsEnabled = true;
                pbtn.Visibility = Visibility.Visible;
            }
        }

        public class STime
        {
            public int m { get; set; }
            public int s { get; set; }
        }
        public STime GetStime(double v)
        {
            var st = new STime();

            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(v));
            //得到多少分钟
            st.m = ts.Minutes;
            st.s = ts.Seconds;
            return st;
        }

        private string SingleSearch(string SongId)
        {
            string songIdStr = SongId;
            if (songIdStr == "" || songIdStr == null || !NeteaseMusicUtils.CheckNum(songIdStr))
            {
                MessageBox.Show(ErrorMsg.INPUT_ID_ILLEGAG, "提示");
                return "";
            }
            long songId = long.Parse(songIdStr);
            SongVO songVO = RequestSongVO(songId);
            if (!songVO.Success)
            {
                MessageBox.Show(songVO.Message, "提示");
                return "";
            }
            SongUrl = songVO.Links;
            LyricVO lyricVO = RequestLyricVO(songId, globalSearchInfo);
            if (!lyricVO.Success)
            {
                MessageBox.Show(songVO.Message, "提示");
                return "";
            }
            string outputLyric = NeteaseMusicUtils.GetOutputLyric(lyricVO.Lyric, lyricVO.TLyric, globalSearchInfo);
            string lyc = outputLyric == "" ? ErrorMsg.LRC_NOT_EXIST : outputLyric;
            return lyc;

        }
        private void ReloadConfig()
        {
            globalSearchInfo.SearchId = "";
            globalSearchInfo.SerchType = search_type_enum;
            globalSearchInfo.OutputFileNameType = output_filename_type_enum;
            globalSearchInfo.ShowLrcType = SHOW_LRC_TYPE_ENUM.MERGE_TRANSLATE;
            globalSearchInfo.Encoding = output_encoding_enum;
            globalSearchInfo.Constraint2Dot = false;
            globalSearchInfo.BatchSearch = false;
            globalSearchInfo.LrcMergeSeparator = "";
        }
        private SongVO RequestSongVO(long songId)
        {
            SongUrls songUrls = api.GetSongsUrl(new long[] { songId });
            DetailResult detailResult = api.GetDetail(songId);

            return NeteaseMusicUtils.GetSongVO(songUrls, detailResult);
        }

        private LyricVO RequestLyricVO(long songId, SearchInfo searchInfo)
        {
            LyricResult lyricResult = api.GetLyric(songId);
            return NeteaseMusicUtils.GetLyricVO(lyricResult, searchInfo);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Play(true);

            dt.Start();
            me.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Play(false);

            dt.Stop();
            me.Pause();
        }
    }
}
