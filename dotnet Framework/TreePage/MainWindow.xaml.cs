using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TreePage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<AssemblyNumber> AssemblyNumbers { get; set; }
        public AssemblyNumber SelAssyNum { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            AssemblyNumbers = new ObservableCollection<AssemblyNumber>();

            AssemblyNumbers.Add(new AssemblyNumber("LG-D-", "1"));
            AssemblyNumbers.Add(new AssemblyNumber("LG-D-", "2"));
            AssemblyNumbers.Add(new AssemblyNumber("LG-D-", "3"));

            SelAssyNum = new AssemblyNumber("LG-D-", "1");

            this.DataContext = this;
        }
    }
}
