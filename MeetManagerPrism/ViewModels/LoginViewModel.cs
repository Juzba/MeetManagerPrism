/*
using MeetManagerWPF.Services;
using MeetManagerWPF.View.Pages;

namespace MeetManagerWPF.ViewModel;

public partial class LoginViewModel(IDataService dataService, UserStore userStore, IHashService hashService, INavigationService navigation) : ObservableObject
{
    private readonly IDataService _dataService = dataService;
    private readonly UserStore _userStore = userStore;
    private readonly IHashService _hashService = hashService;
    private readonly INavigationService _navigation = navigation;

    // ERROR MESSAGE //
    [ObservableProperty]
    private string? errorMessage;


    // USERNAME //
    [ObservableProperty]
    private string? email;



    // PASSWORD //
    [ObservableProperty]
    private string? password;



    // LOGIN COMMAND //
    [RelayCommand]
    private async Task Login()
    {
        ErrorMessage = "";

        var user = await _dataService.GetUser(Email ?? "");
        if (user == null || !_hashService.VerifyPassword(Password ?? "", user.PasswordHash))
        {
            ErrorMessage = "Chyba přihlášení !!";
            Password = "";
            return;
        }

        // LOGIN CONFIRMED
        Email = "";
        Password = "";

        _userStore.User = user;
        _userStore.IsUserLogged = true;

        _navigation.NavigateTo<HomePage>();
    }


    // INSTANT ACCESS //
    [RelayCommand]
    private async Task InstaAccess()
    {
        var user = await _dataService.GetUser("Juzba@gmail.com");

        if (user == null) return;
        _userStore.User = user;
        _userStore.IsUserLogged = true;

        _navigation.NavigateTo<ManagerPage>();

    }
}
*/