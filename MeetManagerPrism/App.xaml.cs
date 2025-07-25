﻿using MeetManagerPrism.Data;
using MeetManagerPrism.Services;
using MeetManagerPrism.ViewModels;
using MeetManagerPrism.ViewModels.Admin;
using MeetManagerPrism.ViewModels.Manager;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Views;
using MeetManagerPrism.Views.Admin;
using MeetManagerPrism.Views.Manager;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using MeetManagerPrism.ViewModels.Users;
using MeetManagerPrism.Views.Users;

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
        containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
        containerRegistry.RegisterForNavigation<AdminUsersPage, AdminUsersViewModel>();
        containerRegistry.RegisterForNavigation<AdminMainPage, AdminMainViewModel>();
        containerRegistry.RegisterForNavigation<AdminRoomsPage, AdminRoomsViewModel>();
        containerRegistry.RegisterForNavigation<AdminEventTypesPage, AdminEventTypesViewModel>();
        containerRegistry.RegisterForNavigation<ManagerPage, ManagerMainViewModel>();
        containerRegistry.RegisterForNavigation<CreateEventPage, CreateEventViewModel>();
        containerRegistry.RegisterForNavigation<ManagerEventsPage, ManagerEventsViewModel>();
        containerRegistry.RegisterForNavigation<Dashboard, DashboardViewModel>();
    }


    protected override Window CreateShell()
    {
        var mainWindow = Container.Resolve<MainWindow>();
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
