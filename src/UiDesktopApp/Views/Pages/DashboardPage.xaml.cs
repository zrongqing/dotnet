using UiDesktopApp.Services;
using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.Views.Pages;

public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public DashboardViewModel ViewModel { get; }
}