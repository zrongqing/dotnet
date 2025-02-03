using System.Windows.Controls;
using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiDesktopApp.Views.Pages;

public partial class ListViewPage : INavigableView<ListViewViewModel>
{
    public ListViewPage(ListViewViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }

    public ListViewViewModel ViewModel { get; }
}