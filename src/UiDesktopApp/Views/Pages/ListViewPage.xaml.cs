using App.UI;
using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.Views.Pages;

public partial class ListViewPage : INavigableView<ListViewModel>,IAppView
{
    public ListViewPage(ListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public ListViewModel ViewModel { get; }
}