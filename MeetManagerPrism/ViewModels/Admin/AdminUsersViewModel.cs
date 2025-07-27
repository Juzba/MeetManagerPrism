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

        private AsyncDelegateCommand OnInitializeCommand { get; }
        private AsyncDelegateCommand LoadUsersListCommand { get; }
        private AsyncDelegateCommand LoadRolesListCommand { get; }

        public AsyncDelegateCommand SaveCommand { get; }
        public AsyncDelegateCommand<object?> RemoveUserCommand { get; }

        public AdminUsersViewModel(IDataService dataService, UserStore userStore, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _userStore = userStore;
            _eventAggregator = eventAggregator;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
            LoadUsersListCommand = new AsyncDelegateCommand(LoadUsersList);
            LoadRolesListCommand = new AsyncDelegateCommand(LoadRolesList);
            SaveCommand = new AsyncDelegateCommand(SaveChanges);
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
            await LoadUsersListCommand.Execute();

            // LOAD ROLES FROM DB //
            await LoadRolesListCommand.Execute();
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
            await LoadUsersListCommand.Execute();
        }
    }
}
