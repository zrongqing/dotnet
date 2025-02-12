using System.IO;
using System.Reflection;
using System.Windows.Threading;
using App.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UiDesktopApp.Services;
using UiDesktopApp.ViewModels.Pages;
using UiDesktopApp.ViewModels.Windows;
using UiDesktopApp.Views.Pages;
using UiDesktopApp.Views.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;

namespace UiDesktopApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        // Properties 相当于一个字典，可以在XAML或者代码中直接使用
        Properties["StartupPath"] = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Properties["StartupArgs"] = Environment.GetCommandLineArgs();
        Properties["UserDataPath"] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Properties["UserName"] = Environment.UserName;
    }
    
    
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(
            c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ApplicationHostService>();

            WpfUIServers(services);

            // Main window with navigation
            AppViews(services);


        }).Build();

    private static void AppViews(IServiceCollection services)
    {
        services.AddSingleton<INavigationWindow, MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<DashboardPage>();
        services.AddSingleton<DashboardViewModel>();

        services.AddSingleton<DataPage>();
        services.AddSingleton<DataViewModel>();

        services.AddSingleton<SettingsPage>();
        services.AddSingleton<SettingsViewModel>();

        services.AddSingleton<ToolPage>();
        services.AddSingleton<ToolViewModel>();
        
        // 注册 INavigationAware
        var assembly = Assembly.GetEntryAssembly();
        if (assembly is null)
            return;
        
        var types = assembly.GetTypes();
        var appViewTypes = types
            .Where(t => t.GetInterfaces().Contains(typeof(IAppView)) && !t.IsInterface && !t.IsAbstract);
        var appViewModelsType = types.Where(t => t.GetInterfaces().Contains(typeof(IAppViewModel)));
        
        foreach (var type in appViewTypes)
        {
            var serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Singleton);
            if (services.Contains(serviceDescriptor))
                continue;
        
            services.AddSingleton(type);
        }

        foreach (var type in appViewModelsType)
        {
            var serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Singleton);
            if (services.Contains(serviceDescriptor))
                continue;
        
            services.AddSingleton(type);
        }
    }

    private static void WpfUIServers(IServiceCollection services)
    {
        // Page resolver service
        services.AddSingleton<INavigationViewPageProvider, PageService>();

        // Theme manipulation
        services.AddSingleton<IThemeService, ThemeService>();

        // TaskBar manipulation
        services.AddSingleton<ITaskBarService, TaskBarService>();

        // Service containing navigation, same as INavigationWindow... but without window
        services.AddSingleton<INavigationService, NavigationService>();
    }

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T"> Type of the service to get. </typeparam>
    /// <returns> Instance of the service or <see langword="null" />. </returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Get Application Version
    /// </summary>
    /// <returns> Assembly.GetExecutingAssembly().GetName().Version </returns>
    public static Version? GetAppVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version;
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }
}