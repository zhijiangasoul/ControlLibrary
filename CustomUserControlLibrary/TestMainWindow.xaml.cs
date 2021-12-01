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
using System.Windows.Shapes;

namespace CustomUserControlLibrary
{
    /// <summary>
    /// TestMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestMainWindow : Window
    {
        public TestMainWindow()
        {
            InitializeComponent();
            Loaded += TestMainWindow_Loaded;
        }

        private void TestMainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
