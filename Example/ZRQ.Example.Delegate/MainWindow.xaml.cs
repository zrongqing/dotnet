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

namespace ZRQ.Example.Delegate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Update = new Update();
        }

        private Update Update { get; set; }

        private void Btn_Init_Click(object sender, RoutedEventArgs e)
        {
            this.Update = new Update();
            Update.UpdateComparator = AAAA;
        }

        public int AAAA(int left, int right)
        {
            return left + right;
        }


        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            this.Update.Excute();
        }
    }
}
