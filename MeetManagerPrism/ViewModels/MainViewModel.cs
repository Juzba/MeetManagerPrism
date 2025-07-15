/*
using MeetManagerWPF.Services;
using MeetManagerWPF.View.Pages;
using System.Windows;
using System.Windows.Navigation;


namespace MeetManagerWPF.ViewModel
{
    public partial class MainViewModel 
    {
        private readonly INavigationService _navigation;
        private readonly UserStore _userStore;

        public MainViewModel( INavigationService navigation, UserStore userStore)
        {
            _userStore = userStore;
            _navigation = navigation;

            OnLogin();
        }

        // ON LOGIN //
        private void OnLogin()
        {
            _userStore.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(UserStore.IsUserLogged))
                {
                    // Hide register and login
                    LoginVisibility = _userStore.IsUserLogged;

                    // Show username
                    UserName = $"{_userStore.User?.Name ?? "UserName"}";

                    // Role Admin? Show page for admin
                    if (_userStore.User?.Role?.RoleName == "Admin")
                    {
                        AdminPageVisibility = Visibility.Visible;
                        ManagerPageVisibility = Visibility.Visible;
                    }

                    // Role Manager? Show page for manager
                    if (_userStore.User?.Role?.RoleName == "Manager") ManagerPageVisibility = Visibility.Visible;

                }
            };
        }

        // USERNAME //
        [ObservableProperty]
        private string? userName;

        // LOGINVISIBILITY //
        [ObservableProperty]
        private bool loginVisibility;

        // ADMIN PAGE VISIBILITY //
        [ObservableProperty]
        private Visibility adminPageVisibility = Visibility.Collapsed;

        // MANAGER PAGE VISIBILITY //
        [ObservableProperty]
        private Visibility managerPageVisibility = Visibility.Collapsed;

        [RelayCommand]
        private void Logout()
        {
            _userStore.IsUserLogged = false;
            _userStore.User = null;
            _navigation.NavigateTo<LoginPage>();
            AdminPageVisibility = Visibility.Collapsed;
            ManagerPageVisibility = Visibility.Collapsed;
        }


        /// <summary>
        /// NAVIGATION
        /// </summary>

        // TO LOGIN PAGE //
        [RelayCommand]
        private void NavigateToLogin() => _navigation.NavigateTo<LoginPage>();

        // TO REGISTER PAGE //
        [RelayCommand]
        private void NavigateToRegister() => _navigation.NavigateTo<RegisterPage>();

        // TO HOME PAGE //
        [RelayCommand]
        private void NavigateToHomePage() => _navigation.NavigateTo<HomePage>();

        // TO EVENTS PAGE //
        [RelayCommand]
        private void NavigateToEventsPage() => _navigation.NavigateTo<EventsPage>();

        // TO ADD EVENT PAGE //
        [RelayCommand]
        private void NavigateToAddEventPage() => _navigation.NavigateTo<ManagerPage>();

        // TO ADMIN PAGE //
        [RelayCommand]
        private void NavigateToAdminPage()
        {
            if (_userStore.User?.Role.RoleName == "Admin")
                _navigation.NavigateTo<AdminPage>();
        }

        // LOGOUT //
    }
}
*/