using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// ResetEventControl.xaml 的交互逻辑
    /// </summary>
    public partial class ResetEventControl : UserControl
    {
        int i = 1;
      
        public ResetEventControl()
        {
            InitializeComponent();
        }
        AutoResetEvent MainAutoResetEvent = new AutoResetEvent(false);

        AutoResetEvent OddAutoResetEvent = new AutoResetEvent(false);

        AutoResetEvent EvenAutoResetEvent = new AutoResetEvent(false);

        Thread thread1;
        Thread thread2;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            object obj1 = new object();
            thread1 = new Thread(new ParameterizedThreadStart(AutoResetEventThread1));
            thread1.ApartmentState = ApartmentState.STA;
            thread1.IsBackground = true;
            thread1.Start(obj1);
            object obj2 = new object();
            thread2 = new Thread(new ParameterizedThreadStart(AutoResetEventThread2));
            thread2.ApartmentState = ApartmentState.STA;
            thread2.IsBackground = true;
            thread2.Start(obj2);
        }
        public void AutoResetEventThread1(object obj)
        {
            while (!MainAutoResetEvent.WaitOne(1))
            {
                OutputOddNum();
            }

        }
        public void AutoResetEventThread2(object obj)
        {
            while (!MainAutoResetEvent.WaitOne(1))
            {
                OutputEvenNum();
            }

        }

        public void OutputOddNum()
        {
            if (i%2!=0)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ShowTextBox.Text = ShowTextBox.Text + " " +i;
                    i++;                
                }));

                OddAutoResetEvent.Set();
                EvenAutoResetEvent.WaitOne();
            }
            if (i>=100)
            {
                thread1.Abort();
            }


        }
        public void OutputEvenNum()
        {
            if(i%2==0)
            {
                EvenAutoResetEvent.Set();
                OddAutoResetEvent.WaitOne();
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ShowTextBox.Text = ShowTextBox.Text + " " + i;
                    i++;
                }));
            }
            if (i >= 100)
            {
                thread2.Abort();
            }
        }




    }
}
