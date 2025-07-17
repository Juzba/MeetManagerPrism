using MeetManagerPrism.Data;
using MeetManagerPrism.Services;
using MeetManagerPrism.ViewModels;
using MeetManagerPrism.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace MeetManagerPrism;


public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // SERVICES //
        containerRegistry.Register<AppDbContext>(() =>
        new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=MeetManagerPrism; Trusted_Connection=True;")
            .Options
        ));
        containerRegistry.RegisterSingleton<UserStore>();
        containerRegistry.Register<IDataService, DataService>();
        containerRegistry.Register<ILoginService, LoginService>();
        containerRegistry.Register<MainWindow>();


        // VIEWS //
        containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
        containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        containerRegistry.RegisterForNavigation<HomePage>();
    }


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


}
