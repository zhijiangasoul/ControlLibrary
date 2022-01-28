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
    /// VoteControl.xaml 的交互逻辑
    /// </summary>
    public partial class VoteControl : UserControl
    {
        List<KeyWordValueLIst> keyWordValueList = new List<KeyWordValueLIst>();

        public VoteControl(string VoteTitle,List<KeyWordValueLIst> keyWordValueLIsts)
        {
            InitializeComponent();
            keyWordValueList = keyWordValueLIsts;
            TipsTextBlock.Text = VoteTitle;
            Loaded += VoteControl_Loaded;

           
        }

        private void VoteControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (KeyWordValueLIst lIst in keyWordValueList)
            {
                HeadImg headImg = new HeadImg(lIst);
                headImg.Margin = new Thickness(30,0,0,0);
                HeadIconPanel.Children.Add(headImg);
            }
        }
    }
}
