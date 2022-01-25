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


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HWTosat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void click(object sender, EventArgs e)
        {

        }

        public void closed(object sender, EventArgs e)
        {
            Console.WriteLine($"===>Closed!");
        }

        private void textBox_TextInput()
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("asd", "asd");
        }
    }
}
