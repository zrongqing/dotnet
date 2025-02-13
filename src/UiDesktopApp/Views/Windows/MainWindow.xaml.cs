using UiDesktopApp.ViewModels.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace UiDesktopApp.Views.Windows;

public partial class MainWindow : INavigationWindow
{
    public MainWindow(
        MainWindowViewModel   viewModel,
        INavigationService    navigationService,
        ISnackbarService      snackbarService,
        IContentDialogService contentDialogService
    )
    {
        ViewModel = viewModel;
        DataContext = this;

        SystemThemeWatcher.Watch(this);

        InitializeComponent();
        
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        navigationService.SetNavigationControl(RootNavigation);
        contentDialogService.SetDialogHost(RootContentDialog);
    }

    public MainWindowViewModel ViewModel { get; }

    INavigationView INavigationWindow.GetNavigation()
    {
        throw new NotImplementedException();
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Raises the closed event.
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        Application.Current.Shutdown();
    }

    #region INavigationWindow methods

    public bool Navigate(Type pageType)
    {
        return RootNavigation.Navigate(pageType);
    }


    public void SetPageService(INavigationViewPageProvider navigationViewPageProvider)
    {
        throw new NotImplementedException();
    }

    public void ShowWindow()
    {
        Show();
    }

    public void CloseWindow()
    {
        Close();
    }

    #endregion INavigationWindow methods
}