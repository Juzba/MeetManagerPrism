using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin
{
    public partial class AdminUsersViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoginService _loginService;

        private AsyncDelegateCommand OnInitializeCommand { get; }
        public AsyncDelegateCommand SaveCommand { get; }
        public AsyncDelegateCommand AddUserCommand { get; }
        public AsyncDelegateCommand<object?> RemoveUserCommand { get; }

        public AdminUsersViewModel(ILoginService loginService, IDataService dataService, UserStore userStore, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _userStore = userStore;
            _eventAggregator = eventAggregator;
            _loginService = loginService;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
            SaveCommand = new AsyncDelegateCommand(SaveChanges);
            AddUserCommand = new AsyncDelegateCommand(AddUser);
            RemoveUserCommand = new AsyncDelegateCommand<object?>(RemoveUser);

            OnInitializeCommand.Execute();
            _eventAggregator = eventAggregator;
        }

        // I-NAVIGATION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext) { _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Admin Page"); }
        public void OnNavigatedFrom(NavigationContext navigationContext) { }


        private async Task OnInitialize()
        {
            // LOAD USERS FROM DB //
            await LoadUsersList();

            // LOAD ROLES FROM DB //
            await LoadRolesList();
        }






        // ROLES LIST //
        private ObservableCollection<Role> rolesList = [];
        public ObservableCollection<Role> RolesList
        {
            get { return rolesList; }
            set { SetProperty(ref rolesList, value); }
        }


        // USERS LIST //
        private ObservableCollection<User> users = [];
        public ObservableCollection<User> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }


        // ERROR MESSAGE //
        private string? errorMessage;
        public string? ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        // NEW USER //
        private User newUser = new();
        public User NewUser
        {
            get { return newUser; }
            set { SetProperty(ref newUser, value); }
        }


        // LOAD USERS //
        private async Task LoadUsersList()
        {
            var usersData = await _dataService.GetUsersList();
            Users = new ObservableCollection<User>(usersData);
        }


        // LOAD ROLES //
        private async Task LoadRolesList()
        {
            var rolesData = await _dataService.GetRolesList();
            RolesList = new ObservableCollection<Role>(rolesData);
        }


        // SAVE CHANGES //
        private async Task SaveChanges()
        {
            // There must be at least one admin here. //
            if (Users.Any(p => p.RoleId.Contains("Admin"))) await _dataService.SaveChangesDB();

            else ErrorMessage = "Musí být aspoň jeden Admin.";

        }


        // ADD USER //
        private async Task AddUser()
        {
            if (string.IsNullOrWhiteSpace(NewUser.Email) || string.IsNullOrWhiteSpace(NewUser.PasswordHash))
            {
                ErrorMessage = "Chybí Email nebo heslo!";
                return;
            }

            if (await _dataService.GetUser(NewUser.Email) != null)
            {
                ErrorMessage = "Tento uživatel už existuje!";
                return;
            }

            NewUser.PasswordHash = _loginService.HashPassword(NewUser.PasswordHash);

            await _dataService.AddUser(NewUser);

            NewUser = new();
            ErrorMessage = null;
            await LoadUsersList();
        }


        // REMOVE USER //
        private async Task RemoveUser(object? param)
        {
            if (param is not User user) return;

            if (user.Name == _userStore.User?.Name)
            {
                ErrorMessage = "Admin nemůže smazat sám sebe.";
                return;
            }

            await _dataService.DeleteUser(user);
            await LoadUsersList();
        }
    }
}
