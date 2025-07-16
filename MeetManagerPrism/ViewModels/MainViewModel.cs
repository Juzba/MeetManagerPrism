using MeetManagerPrism.View.Pages;
using System.Windows;

namespace MeetManagerPrism.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        
        public DelegateCommand LogoutCommand { get; }


        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            LogoutCommand = new DelegateCommand(Logout);


        }






        // ON LOGIN //
        //private void OnLogin()
        //{
        //    _userStore.PropertyChanged += (sender, e) =>
        //    {
        //        if (e.PropertyName == nameof(UserStore.IsUserLogged))
        //        {
        //            // Hide register and login
        //            LoginVisibility = _userStore.IsUserLogged;

        //            // Show username
        //            UserName = $"{_userStore.User?.Name ?? "UserName"}";

        //            // Role Admin? Show page for admin
        //            if (_userStore.User?.Role?.RoleName == "Admin")
        //            {
        //                AdminPageVisibility = Visibility.Visible;
        //                ManagerPageVisibility = Visibility.Visible;
        //            }

        //            // Role Manager? Show page for manager
        //            if (_userStore.User?.Role?.RoleName == "Manager") ManagerPageVisibility = Visibility.Visible;

        //        }
        //    };
        //}

        // USERNAME //
        private string? userName;

        public string? UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
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

        private void Logout()
        {
            //_userStore.IsUserLogged = false;
            //_userStore.User = null;
            //_navigation.NavigateTo<LoginPage>();

            //AdminPageVisibility = Visibility.Collapsed;
            //ManagerPageVisibility = Visibility.Collapsed;
            //_regionManager.RequestNavigate(constants)
        }


        /// <summary>
        /// NAVIGATION
        /// </summary>

        //// TO LOGIN PAGE //
        //[RelayCommand]
        //private void NavigateToLogin() => _navigation.NavigateTo<LoginPage>();

        //// TO REGISTER PAGE //
        //[RelayCommand]
        //private void NavigateToRegister() => _navigation.NavigateTo<RegisterPage>();

        //// TO HOME PAGE //
        //[RelayCommand]
        //private void NavigateToHomePage() => _navigation.NavigateTo<HomePage>();

        //// TO EVENTS PAGE //
        //[RelayCommand]
        //private void NavigateToEventsPage() => _navigation.NavigateTo<EventsPage>();

        //// TO ADD EVENT PAGE //
        //[RelayCommand]
        //private void NavigateToAddEventPage() => _navigation.NavigateTo<ManagerPage>();

        //// TO ADMIN PAGE //
        //[RelayCommand]
        //private void NavigateToAdminPage()
        //{
        //    if (_userStore.User?.Role.RoleName == "Admin")
        //        _navigation.NavigateTo<AdminPage>();
        //}

        // LOGOUT //

    }
}
