using MeetManagerPrism.View.Pages;
using MeetManagerPrism.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MeetManagerPrism;


public partial class App : PrismApplication
{


    protected override Window CreateShell()
    {
        var mainWindow = Container.Resolve<MainWindow>();
        mainWindow.DataContext = Container.Resolve<MainViewModel>();
        return mainWindow;
    }


    protected override void OnInitialized()
    {
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RequestNavigate("MainRegion", nameof(LoginPage));
        base.OnInitialized();
    }


    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();
    }


    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<RegisterPage>();
        containerRegistry.RegisterForNavigation<LoginPage>();

        containerRegistry.Register<MainViewModel>();
        containerRegistry.Register<MainWindow>();
    }
}
