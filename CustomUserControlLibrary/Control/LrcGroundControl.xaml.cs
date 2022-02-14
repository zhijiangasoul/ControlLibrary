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
    /// LrcGroundControl.xaml 的交互逻辑
    /// </summary>
    public partial class LrcGroundControl : UserControl
    {
        public LrcGroundControl(string Lrc,int FforeSize)
        {
            InitializeComponent();
            WidthCount = this.ActualWidth;
            ColorLabel.Text = Lrc;
            ColorLabel.FontSize = FforeSize;
            WordLabel.Text = Lrc;
            WordLabel.FontSize = FforeSize;
        }
        public double WidthCount = 0;
        Timer StatisTimer;
        public double Time = 0;
        public void InitTime(double timespan)
        {
            WidthCount = this.ActualWidth;
            Time = timespan;
            StatisTimer = new Timer(10);
            StatisTimer.Elapsed += StatisTimer_Elapsed;
            StatisTimer.AutoReset = true;
            StatisTimer.Enabled = true;
        }
        private void StatisTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ColorLayer.Width+= WidthCount / (Time/10);
                if(ColorLayer.Width>= WidthCount)
                {
                    ColorLayer.Width = WidthCount;
                    StatisTimer.Enabled = false;
                    StatisTimer.Stop();                  
                    StatisTimer.Dispose();
                }
            }));
        }
    }
}
