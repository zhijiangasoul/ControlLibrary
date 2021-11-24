using CustomUserControlLibrary.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// CountDownControl.xaml 的交互逻辑
    /// </summary>
    public partial class CountDownControl : UserControl
    {
        private TimeLineModel model;
        public Timer timer;
        public double CountDownTime =0;
        public double Lefttime = 0;
        public bool DirectionFlag;
        public CountDownControl(int time,bool directionFlag)
        {
            InitializeComponent();
            DirectionFlag = directionFlag;
            CountDownTime = time;
            model = new TimeLineModel();
            this.DataContext = model;
            Loaded += CountDownControl_Loaded;
        }

        private void CountDownControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            timer = new Timer(50);
            if(DirectionFlag)
            {
                Lefttime = 0;
                timer.Elapsed += new ElapsedEventHandler(positiveCountTime);
            }
            else 
            {
                Lefttime = CountDownTime;
                timer.Elapsed += new ElapsedEventHandler(ReversecountTime);
            }          
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
        private void positiveCountTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Lefttime <= CountDownTime)
                {
                    Lefttime = Lefttime+0.05;
                    model.LeftTime = CountDownTime - Lefttime;
                    double FCurrVal = (CountDownTime - Lefttime) * 100 / CountDownTime;
                    this.FCurrVal = FCurrVal;
                }
                else if (timer != null)
                {
                    timer.Enabled = false;
                    timer = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ReversecountTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Lefttime > 0)
                {
                    Lefttime = Lefttime-0.05;
                    model.LeftTime = CountDownTime - Lefttime;
                    double FCurrVal = (CountDownTime - Lefttime) * 100 / CountDownTime;
                    this.FCurrVal = FCurrVal;
                }
                else if (timer != null)
                {
                    timer.Enabled = false;
                    timer = null;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public double FCurrVal
        {
            set
            {
                model.FCurrVal = value;
            }
        }

        public class TimeLineModel : BaseBind
        {

            private double LeftTime_ = 0;
            public double LeftTime
            {
                get
                {
                    LeftTime_ = double.Parse(LeftTime_.ToString("0"));
                    return LeftTime_;
                }
                set
                {

                    SetProperty(ref LeftTime_, value);
                }

            }
            /// <summary>
            /// 当前百分比
            /// </summary>
            private double FCurrVal_ = 0;
            public double FCurrVal
            {
                get
                {
                    return FCurrVal_;
                }
                set
                {
                    SetProperty(ref FCurrVal_, value);
                }

            }
        }
    }
}
