/*
using MeetManagerWPF.Model;
using MeetManagerWPF.Services;
using MeetManagerWPF.View.Pages;


namespace MeetManagerPrism.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IHashService _hashService;
        private readonly IDataService _dataService;
        private readonly INavigationService _navigation;

        public RegisterViewModel(IHashService hashService, IDataService dataService, INavigationService navigation)
        {
            _hashService = hashService;
            _dataService = dataService;
            _navigation = navigation;
            ErrorMessage = "";
        }


        // USERNAME //
        [ObservableProperty]
        private string? email;


        // PASSWORD //
        [ObservableProperty]
        private string? passwordA;


        // PASSWORD2 //
        [ObservableProperty]
        private string? passwordB;


        // ERROR MESSAGE //
        [ObservableProperty]
        private string? errorMessage;



        // REGISTER COMMAND //
        [RelayCommand]
        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrEmpty(PasswordA) || !PasswordA.Equals(PasswordB))
            {
                ErrorMessage = "Chybí email nebo obě hesla nejsou stejná.";
                return;
            }

            if (await _dataService.GetUser(Email) != null)
            {
                ErrorMessage = "Uživatel s tímto emailem již existuje.";
                return;
            }

            var hash = _hashService.HashPassword(PasswordA);
            var newUser = new User() { Name = Email, Email = Email, PasswordHash = hash };

            await _dataService.AddUser(newUser);

            ErrorMessage = "";
            PasswordA = "";
            PasswordB = "";
            Email = "";

            _navigation.NavigateTo<LoginPage>();
        }
    }
}

*/