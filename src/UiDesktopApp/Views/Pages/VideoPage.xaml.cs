using System.Windows.Controls;
using UiDesktopApp.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.Views.Pages;

public partial class VideoPage : Page,INavigableView<VideoViewModel>
{
    public VideoPage(VideoViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public VideoViewModel ViewModel { get; }
}