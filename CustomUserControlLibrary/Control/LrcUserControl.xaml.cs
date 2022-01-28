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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomUserControlLibrary.Control
{
    /// <summary>
    /// LrcUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LrcUserControl : UserControl
    {
        public LrcUserControl()
        {
            InitializeComponent();
        }

        public class LrcModel
        {
            /// <summary>
            /// 歌词所在控件
            /// </summary>
            public TextBlock c_LrcTb { get; set; }

            /// <summary>
            /// 歌词字符串
            /// </summary>
            public string LrcText { get; set; }

            /// <summary>
            /// 时间（读取格式参照网易云音乐歌词格式：xx:xx.xx，即分:秒.毫秒，秒小数点保留2位。如：00:28.64）
            /// </summary>
            public double Time { get; set; }
        }



        public Dictionary<double, LrcModel> Lrcs = new Dictionary<double, LrcModel>();

        //添加当前焦点歌词变量
        public LrcModel foucslrc { get; set; }
        //当前焦点所在歌词集合位置
        public int FoucsLrcLocation { get; set; } = -1;
        //非焦点歌词颜色
        public SolidColorBrush NoramlLrcColor = new SolidColorBrush(Colors.Black);
        //焦点歌词颜色
        public SolidColorBrush FoucsLrcColor = new SolidColorBrush(Colors.OrangeRed);

        public void LoadLrc(string lrcstr)
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

                    

                    //歌词显示textblock控件
                    TextBlock c_lrcbk = new TextBlock();
                    c_lrcbk.HorizontalAlignment = HorizontalAlignment.Center;
                    c_lrcbk.FontSize = 40;
                    c_lrcbk.FontFamily = new FontFamily("微软雅黑");
                    //赋值
                    c_lrcbk.Text = lrc;
                    if (c_lrc_items.Children.Count > 0)
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


                    //将歌词显示textblock控件添加到界面中显示
                    c_lrc_items.Children.Add(c_lrcbk);

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
        public void LrcRoll(double nowtime)
        {
            if (foucslrc == null)
            {
                foucslrc = Lrcs.Values.First();
                foucslrc.c_LrcTb.Foreground = FoucsLrcColor;
            }
            else
            {
                //查找焦点歌词
                IEnumerable<KeyValuePair<double, LrcModel>> s = Lrcs.Where(m => nowtime >= m.Key);
                if (s.Count() > 0)
                {
                    LrcModel lm = s.Last().Value;
                    foucslrc.c_LrcTb.Foreground = NoramlLrcColor;
                    foucslrc.c_LrcTb.VerticalAlignment = VerticalAlignment.Center;
                    foucslrc = lm;
                    foucslrc.c_LrcTb.Foreground = FoucsLrcColor;
                    //定位歌词在控件中间区域
                    ResetLrcviewScroll();
                }
            }
        }
        //计算时间差
        public double DateDiff(DateTime DateTime1, DateTime DateTime2)
        {

            double dateDiff = 0;

            try

            {

                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);

                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);

                TimeSpan t = ts1 - ts2;

                TimeSpan ts = ts1.Subtract(ts2).Duration();



                dateDiff = t.Seconds + t.Milliseconds;
            }

            catch

            {

            }

            return dateDiff;

        }


        public void ResetLrcviewScroll()
        {
            //获得焦点歌词位置
            GeneralTransform gf = foucslrc.c_LrcTb.TransformToVisual(c_lrc_items);
            Point p = gf.Transform(new Point(0, 0));
            //滚动条当前位置
            //计算滚动位置（p.Y是焦点歌词控件(c_LrcTb)相对于父级控件c_lrc_items(StackPanel)的位置）
            //拿焦点歌词位置减去滚动区域控件高度除以2的值得到的【大概】就是歌词焦点在滚动区域控件的位置
            double os = p.Y - (c_scrollviewer.ActualHeight / 2) + 20;
            //滚动
            c_scrollviewer.ScrollToVerticalOffset(os);

        }
    }
}
