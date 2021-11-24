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
    /// ConvertControl.xaml 的交互逻辑
    /// </summary>
    public partial class ConvertControl : UserControl
    {
        TestModel testModel = new TestModel();
        public ConvertControl()
        {
            InitializeComponent();
            this.DataContext = testModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testModel.testText =  (int.Parse(testModel.testText) + 1).ToString();
        }
    }
}
