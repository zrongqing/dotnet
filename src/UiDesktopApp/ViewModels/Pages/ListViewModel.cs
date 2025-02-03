using App.UI;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.ViewModels.Pages;

[ObservableObject]
public partial class ListViewModel :INavigationAware,IAppViewModel
{
    public Task OnNavigatedToAsync()
    {
        return Task.CompletedTask;
    }

    public Task OnNavigatedFromAsync()
    {
        return Task.CompletedTask;
    }
} 