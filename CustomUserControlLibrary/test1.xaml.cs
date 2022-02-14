using CustomUserControlLibrary.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CustomUserControlLibrary.Control.LrcUserControl;

namespace CustomUserControlLibrary
{
    /// <summary>
    /// test1.xaml 的交互逻辑
    /// </summary>
    public partial class test1 : Window
    {
        public test1()
        {
            InitializeComponent();
            Loaded += Test1_Loaded;
            KeyUp += Test1_KeyUp;
        }

        private void Test1_KeyUp(object sender, KeyEventArgs e)
        {
           if(e.Key==Key.F5)
            {
                foreach(UIElement uIElement in textPanel.Children)
                {
                    LrcGroundControl lrcGroundControl = uIElement as LrcGroundControl;
                    lrcGroundControl.InitTime(1000);
                }
            }
        }

        public Dictionary<double, LrcModel> Lrcs = new Dictionary<double, LrcModel>();

        public List<object> c_lrc_items = new List<object>();

        public List<LrcModel> LrcTimeSave = new List<LrcModel>();
        private void Test1_Loaded(object sender, RoutedEventArgs e)
        {
            string lrc = ReadTxtContent(@"F:\焦子豪\zhijiangControlLibrary-master\CustomUserControlLibrary\bin\Debug\text\lrc.txt");
            LrcGroundControl lrcGroundControl = new LrcGroundControl("啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊",40);
            textPanel.Children.Add(lrcGroundControl);


        }
        public string ReadTxtContent(string Path)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(Path, Encoding.UTF8);
            string text = sr.ReadToEnd();
            //string content;
            //while ((content = sr.ReadLine()) != null)
            //{
            //    return content.ToString();
            //}
            return text;
        }




        public void LoadLrc(string lrcstr, double Maintime)
        {
            //循环以换行\n切割出歌词
            foreach (string str in lrcstr.Split('\n'))
            {
                //过滤空行，判断是否存在时间
                if (str.Length > 0 && str.IndexOf(":") != -1)
                {
                    TimeSpan time;
                    //歌词时间
                    try
                    {
                        time = GetTime(str);
                    }
                    catch
                    {
                        continue;
                    }
                    //歌词取]后面的就行了
                    string lrc = str.Split(']')[1];
                    double LastTime = 0;
                    double NextTime = 0;
                    if (LrcTimeSave.Count != 0)
                    {
                        LastTime = LrcTimeSave.Last().Time;
                        NextTime = time.TotalMilliseconds - LastTime;
                        LrcTimeSave[LrcTimeSave.Count() - 1].NextTime = NextTime;
                        Lrcs.Last().Value.NextTime = NextTime;
                    }


                    //歌词显示textblock控件
                    LrcGroundControl c_lrcbk = new LrcGroundControl(lrc,40);
                    c_lrcbk.HorizontalAlignment = HorizontalAlignment.Center;
                    c_lrcbk.FontFamily = new FontFamily("微软雅黑");
                    if (c_lrc_items.Count > 0)
                    {
                        c_lrcbk.Margin = new Thickness(0, 10, 0, 0);
                    }
                    if (Lrcs.ContainsKey(time.TotalMilliseconds))
                    {
                        Lrcs.Add(time.TotalMilliseconds + 1, new LrcModel()
                        {
                            c_LrcTb = c_lrcbk,
                            LrcText = lrc,
                            Time = time.TotalMilliseconds


                        });
                    }
                    else
                    {
                        Lrcs.Add(time.TotalMilliseconds, new LrcModel()
                        {
                            c_LrcTb = c_lrcbk,
                            LrcText = lrc,
                            Time = time.TotalMilliseconds

                        });
                    }
                    //添加到集合，方便日后操作
                    LrcTimeSave.Add(Lrcs[time.TotalMilliseconds]);
                    //将歌词显示textblock控件添加到界面中显示
                    c_lrc_items.Add(c_lrcbk);

                }
            }



        }

        //正则表达式提取时间
        public TimeSpan GetTime(string str)
        {
            Regex reg = new Regex(@"\[(?<time>.*)\]", RegexOptions.IgnoreCase);
            string timestr = reg.Match(str).Groups["time"].Value;

            //获得分
            int m = Convert.ToInt32(timestr.Split(':')[0]);
            //判断是否有小数点
            int s = 0, f = 0;
            if (timestr.Split(':')[1].IndexOf(".") != -1)
            {
                //有
                s = Convert.ToInt32(timestr.Split(':')[1].Split('.')[0]);
                //获得毫秒位
                f = Convert.ToInt32(timestr.Split(':')[1].Split('.')[1]);

            }
            else
            {                //没有
                s = Convert.ToInt32(timestr.Split(':')[1]);
            }
            return new TimeSpan(0, 0, m, s, f);
        }
    }
}
