using MeetManagerPrism.Data;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views;
using MeetManagerPrism.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using MeetManagerPrism.ViewModel;

namespace MeetManagerPrism;


public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // SERVICES //
        containerRegistry.Register<AppDbContext>(() =>
        new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=LearningDB; Trusted_Connection=True;")
            .Options
        ));
        containerRegistry.Register<UserStore>();
        containerRegistry.Register<ILoginService, LoginService>();
        containerRegistry.Register<MainWindow>();



        // VIEWMODELS //
        containerRegistry.Register<MainViewModel>();
        //containerRegistry.Register<LoginPageViewModel>();
        //containerRegistry.Register<RegisterPageViewModel>();



        // VIEWS //
        containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
        containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
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
