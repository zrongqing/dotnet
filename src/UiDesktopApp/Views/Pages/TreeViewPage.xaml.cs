using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.Views.Pages;

public partial class TreeViewPage : INavigableView<TreeViewModel>
{
    public TreeViewPage(TreeViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public TreeViewModel ViewModel { get; }
}