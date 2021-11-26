using HWCommonUI.HWWindows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using HWCommonUI.HWControl.HWToast;

namespace WPF_Samples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HWProgressBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HWProgressBarDialog.Instance.StartLoop();
            for (int i = 0; i < int.MaxValue; i++)
            {
                if(HWProgressBarDialog.IsClose)
                {
                    break;
                }
                Debug.WriteLine(i);
            }
            //HWProgressBarDialog.Instance.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //Toast.Show("这是一个友好的提示-上", 1000, ToastPosition.Top);

            //Toast.Show("这是一个友好的提示-中", 1000, ToastPosition.Center);

            //Toast.Show("这是一个友好的提示-下", 1000, ToastPosition.Bottom);
        }

        private void HWTosat_Click(object sender, RoutedEventArgs e)
        {
           HWToast.Show("容器左上角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerTopLeft, Time = 1000 });
           HWToast.Show("容器上中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerTopCenter, Time = 1000 });
           HWToast.Show("容器右上角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerTopRight, Time = 1000 });
           HWToast.Show("容器左中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerLeft, Time = 1000 });
           HWToast.Show("容器中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerCenter, Time = 1000, Closed = closed, Click = click });
           HWToast.Show("容器右中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerRight, Time = 1000 });
           HWToast.Show("容器左下角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerBottomLeft, Time = 1000 });
           HWToast.Show("容器下中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerBottomCenter, Time = 1000 });
           HWToast.Show("容器右下角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.OwnerBottomRight, Time = 1000 });
           
           HWToast.Show("屏幕左上角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenTopLeft, Time = 1000 });
           HWToast.Show("屏幕上中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenTopCenter, Time = 1000 });
           HWToast.Show("屏幕右上角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenTopRight, Time = 1000 });
           HWToast.Show("屏幕左中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenLeft, Time = 1000 });
           HWToast.Show("屏幕中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenCenter, Time = 1000 });
           HWToast.Show("屏幕右中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenRight, Time = 1000 });
           HWToast.Show("屏幕左下角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenBottomLeft, Time = 1000 });
           HWToast.Show("屏幕下中间", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenBottomCenter, Time = 1000 });
           HWToast.Show("屏幕右下角", new ToastOptions { Icon = ToastIcons.Information, Location = ToastLocation.ScreenBottomRight, Time = 1000 });
        }

        private void click(object sender, EventArgs e)
        {
            HWToast toast = sender as HWToast;
            if (toast == null)
            {
                return;
            }
            toast.Close();
            Console.WriteLine($"===>Click!");
        }

        public void closed(object sender, EventArgs e)
        {
            Console.WriteLine($"===>Closed!");
        }

        private void textBox_TextInput()
        {

        }
    }
}
