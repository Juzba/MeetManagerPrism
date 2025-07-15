using MeetManagerPrism.View.Pages;
using MeetManagerWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MeetManagerPrism;


public partial class App : PrismApplication
{
    public static IHost AppHost { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<MainWindow>();

                //ViewModels
                services.AddTransient<MainViewModel>();
            })
            .Build();

        base.OnStartup(e);
    }

    protected override async void OnInitialized()
    {
        await AppHost.StartAsync();
        base.OnInitialized();
    }

    protected override Window CreateShell()
    {
        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = AppHost.Services.GetRequiredService<MainViewModel>();
        return mainWindow;
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<RegisterPage>();
        containerRegistry.RegisterForNavigation<LoginPage>();
    }
}
