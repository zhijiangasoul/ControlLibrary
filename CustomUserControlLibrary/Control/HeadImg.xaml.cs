using CustomUserControlLibrary.Converter;
using CustomUserControlLibrary.Model;
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

namespace CustomUserControlLibrary.Control
{
    /// <summary>
    /// HeadImg.xaml 的交互逻辑
    /// </summary>
    public partial class HeadImg : UserControl
    {
        public TimeLineModel model;
        public HeadImg(KeyWordValueLIst list)
        {
            InitializeComponent();
            this.DataContext = model;
            HeadImgStr.Source = StaticData.GetBitmapImage(list.HeadImg);
            Keyword.Text = list.KeyWord;
            if(!string.IsNullOrEmpty(list.ElipseGround))
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
