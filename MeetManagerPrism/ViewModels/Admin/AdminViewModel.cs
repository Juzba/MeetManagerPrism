using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin
{
    public partial class AdminViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;

        public AsyncDelegateCommand OnInitializeCommand { get; }
        public AsyncDelegateCommand LoadUsersListCommand { get; }
        public AsyncDelegateCommand SaveCommand { get; }
        public AsyncDelegateCommand<object?> RemoveUserCommand { get; }

        public AdminViewModel(IDataService dataService)
        {
            _dataService = dataService;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
            LoadUsersListCommand = new AsyncDelegateCommand(LoadUsersList);
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

        }





        // ROLES LIST //
        private ObservableCollection<Role> rolesList = [new Role { Id = "AdminRoleId-51sa9-sdd18", RoleName = "Admin" }, new Role { Id = "UserRoleId-54sa9-sda87", RoleName = "User" }, new Role { Id = "ManagerRoleId-21ga5-sda13", RoleName = "Manager" }];
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




        // LOAD DATA //
        private async Task LoadUsersList()
        {
            var usersData = await _dataService.GetUsersList();
            Users = new ObservableCollection<User>(usersData);
        }


        // SAVE CHANGES //
        private async Task SaveChanges() => await _dataService.UpdateUsersList();



        // REMOVE USER //
        private async Task RemoveUser(object? param)
        {
            if (param is not User user) return;

            await _dataService.DeleteUser(user);
            await LoadUsersListCommand.Execute();
        }










    }
}
