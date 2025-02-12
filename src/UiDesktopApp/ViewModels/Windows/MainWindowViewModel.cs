using System.Collections.ObjectModel;
using UiDesktopApp.Views.Pages;
using Wpf.Ui.Controls;

namespace UiDesktopApp.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _applicationTitle = "WPF UI - UiDesktopApp";

    [ObservableProperty] 
    private ObservableCollection<object> _footerMenuItems = new()
    {
        new NavigationViewItem
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        }
    };

    [ObservableProperty] 
    private ObservableCollection<object> _menuItems = new()
    {
        new NavigationViewItem
        {
            Content = "Home",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageType = typeof(DashboardPage)
        },
        new NavigationViewItem
        {
            Content = "Data",
            Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            TargetPageType = typeof(DataPage)
        },
        new NavigationViewItem
        {
            Content = "Tool",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Toolbox24 },
            TargetPageType = typeof(ToolPage)
        },
        new NavigationViewItem
        {
            Content = "ListView",
            Icon = new SymbolIcon { Symbol = SymbolRegular.TextBulletListTree24 },
            TargetPageType = typeof(TreeViewPage)
        }
    };

    [ObservableProperty] 
    private ObservableCollection<MenuItem> _trayMenuItems = new()
    {
        new MenuItem { Header = "Home", Tag = "tray_home" }
    };
}