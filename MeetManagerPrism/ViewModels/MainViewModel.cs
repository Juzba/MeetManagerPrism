using MeetManagerPrism.Common;
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views;
using MeetManagerPrism.Views.Admin;
using MeetManagerPrism.Views.Manager;
using MeetManagerPrism.Views.Users;
using System.Windows;

namespace MeetManagerPrism.ViewModels;

public partial class MainViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private readonly UserStore _userStore;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand LogoutCommand { get; }
    public DelegateCommand NavLoginCommand { get; }
    public DelegateCommand NavRegisterCommand { get; }
    public DelegateCommand NavHomeCommand { get; }
    public DelegateCommand NavDashboardCommand { get; }
    public DelegateCommand NavManagerCommand { get; }
    public DelegateCommand NavAdminCommand { get; }


    public MainViewModel(IRegionManager regionManager, UserStore userStore, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _userStore = userStore;
        _eventAggregator = eventAggregator;

        OnLogin();

        LogoutCommand = new DelegateCommand(Logout);

        // PAGE TITLE EVENT //
        _eventAggregator.GetEvent<MainViewTitleEvent>().Subscribe((string title) => PageTitle = title);

        // TO LOGIN PAGE //
        NavLoginCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.MainRegion, nameof(LoginPage)));

        // TO REGISTER PAGE //
        NavRegisterCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.MainRegion, nameof(RegisterPage)));

        // TO HOME PAGE //
        NavHomeCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.MainRegion, nameof(HomePage)));

        // TO DASHBOARD PAGE //
        NavDashboardCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.MainRegion, nameof(Dashboard)));

        // TO MANAGER PAGE //
        NavManagerCommand = new DelegateCommand(() =>
        {
            if (_userStore.User?.Role.RoleName == "Admin" || _userStore.User?.Role.RoleName == "Manager")
                _regionManager.RequestNavigate(Const.MainRegion, nameof(ManagerPage));
        });

        // TO ADMIN MAIN PAGE //
        NavAdminCommand = new DelegateCommand(() =>
        {
            if (_userStore.User?.Role.RoleName == "Admin")
                _regionManager.RequestNavigate(Const.MainRegion, nameof(AdminMainPage));
        });
    }


    // AFTER LOGIN //
    private void OnLogin()
    {
        _userStore.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(UserStore.IsUserLogged))
            {
                // Hide register and login
                LoginVisibility = _userStore.IsUserLogged;

                // Show username
                UserName = $"{_userStore.User?.Name ?? _userStore.User?.Email ?? "UserName"}";

                // Show users pages
                UserPageVisibility = Visibility.Visible;

                // Role Admin? Show page for admin
                if (_userStore.User?.Role?.RoleName == "Admin")
                {
                    AdminPageVisibility = Visibility.Visible;
                    ManagerPageVisibility = Visibility.Visible;
                }

                // Role Manager? Show page for manager
                if (_userStore.User?.Role?.RoleName == "Manager")
                {
                    ManagerPageVisibility = Visibility.Visible;
                }
            }
        };
    }

    // USERNAME //
    private string? userName;
    public string? UserName
    {
        get { return userName; }
        set { SetProperty(ref userName, value); }
    }


    // PAGE TITLE //
    private string? pageTitle = "Login";
    public string? PageTitle
    {
        get { return pageTitle; }
        set { SetProperty(ref pageTitle, value); }
    }


    // LOGINVISIBILITY //
    private bool loginVisibility;
    public bool LoginVisibility
    {
        get { return loginVisibility; }
        set { SetProperty(ref loginVisibility, value); }
    }




    // ADMIN PAGE VISIBILITY //
    private Visibility adminPageVisibility = Visibility.Collapsed;
    public Visibility AdminPageVisibility
    {
        get { return adminPageVisibility; }
        set { SetProperty(ref adminPageVisibility, value); }
    }


    // MANAGER PAGE VISIBILITY //
    private Visibility managerPageVisibility = Visibility.Collapsed;
    public Visibility ManagerPageVisibility
    {
        get { return managerPageVisibility; }
        set { SetProperty(ref managerPageVisibility, value); }
    }

    // USERS PAGES VISIBILITY //
    private Visibility userPageVisibility = Visibility.Collapsed;
    public Visibility UserPageVisibility
    {
        get { return userPageVisibility; }
        set { SetProperty(ref userPageVisibility, value); }
    }

    // LOGOUT //
    private void Logout()
    {
        _userStore.IsUserLogged = false;
        _userStore.User = null;

        AdminPageVisibility = Visibility.Collapsed;
        ManagerPageVisibility = Visibility.Collapsed;
        UserPageVisibility = Visibility.Collapsed;
        _regionManager.RequestNavigate(Const.MainRegion, nameof(LoginPage));
    }
}
