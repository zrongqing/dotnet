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

namespace TreePage
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeViewLineConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    /// <summary>
    /// TreeView.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewPage : Page
    {
        public TreeViewPage()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            TreeNode treeNode = TreeDataManger.CreatePrafabItemNode();
            this.DataContext = treeNode;
        }

        private void CreateDimenstion_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void refreshNumber_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void FilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TypeTree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TypeTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TypeTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TypeTree_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TypeTree_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void TypeTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void TreeViewContextMenu_Opened(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
