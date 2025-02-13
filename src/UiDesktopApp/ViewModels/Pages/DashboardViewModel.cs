using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.ViewModels.Pages;

public partial class DashboardViewModel : ViewModel
{
    [ObservableProperty] private int _counter;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }

    private bool _isInitialized;

    public Task OnNavigatedToAsync()
    {
        if (!_isInitialized)
            InitializeViewModel();
        
        return Task.CompletedTask;
    }

    private void InitializeViewModel()
    {
        
    }

    public Task OnNavigatedFromAsync()
    {
        return Task.CompletedTask;
    }
}