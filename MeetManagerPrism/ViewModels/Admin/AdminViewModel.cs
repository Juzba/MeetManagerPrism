using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin
{
    public partial class AdminViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;

        public AsyncDelegateCommand OnInitializeCommand { get; }
        public AsyncDelegateCommand LoadUsersListCommand { get; }
        public AsyncDelegateCommand LoadRolesListCommand { get; }
        public AsyncDelegateCommand SaveCommand { get; }
        public AsyncDelegateCommand<object?> RemoveUserCommand { get; }

        public AdminViewModel(IDataService dataService, UserStore userStore)
        {
            _dataService = dataService;
            _userStore = userStore;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
            LoadUsersListCommand = new AsyncDelegateCommand(LoadUsersList);
            LoadRolesListCommand = new AsyncDelegateCommand(LoadRolesList);
            SaveCommand = new AsyncDelegateCommand(SaveChanges);
            RemoveUserCommand = new AsyncDelegateCommand<object?>(RemoveUser);

            OnInitializeCommand.Execute();
        }

        // INAVIGATIONAWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext) { }
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
            if (Users.Any(p => p.RoleId.Contains("Admin"))) await _dataService.UpdateUsersList();

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
