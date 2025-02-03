using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.Views.Pages;

/// <summary>
/// ZipPage.xaml 的交互逻辑
/// </summary>
public partial class ZipPage : INavigableView<ZipPageViewModel>
{
    public ZipPage(ZipPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public ZipPageViewModel ViewModel { get; }
}