using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TestCombox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemListViewModel TestBindingData { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            TestBindingData = new ItemListViewModel();

            this.DataContext = TestBindingData;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(TestBindingData.SelIndex == 0)
            {
                TestBindingData.SelItem = TestBindingData.SizeBindingList[0];
            }
            else
            {
                TestBindingData.SelIndex = 0;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(e.RemovedItems);
            Console.WriteLine(e.AddedItems);
        }
    }
}
