using MeetManagerPrism.Services;
using MeetManagerPrism.Views;

namespace MeetManagerPrism.ViewModels;

public partial class LoginPageViewModel : BindableBase
{
    //private readonly IDataService _dataService;
    //private readonly UserStore _userStore;
    //private readonly ILoginService _loginService;
    //private readonly IRegionManager _regionManager;
    //const string MainRegion = "MainRegion";

    ////public AsyncDelegateCommand LoginCommand { get; }


    //public LoginPageViewModel(IDataService dataService, UserStore userStore, ILoginService loginService, IRegionManager regionManager)
    //{
    //    _dataService = dataService;
    //    _userStore = userStore;
    //    _loginService = loginService;
    //    _regionManager = regionManager;

    //    //LoginCommand = new AsyncDelegateCommand(Login);
    //}


    //// ERROR MESSAGE //
    //private string? errorMessage;
    //public string? ErrorMessage
    //{
    //    get { return errorMessage; }
    //    set { SetProperty(ref errorMessage, value); }
    //}


    //// EMAIL //
    //private string? email;
    //public string? Email
    //{
    //    get { return email; }
    //    set { SetProperty(ref email, value); }
    //}

    //// PASSWORD //
    //private string? password;
    //public string? Password
    //{
    //    get { return password; }
    //    set { SetProperty(ref password, value); }
    //}


    // LOGIN COMMAND //
    //private async Task Login()
    //{
    //    ErrorMessage = "";

    //    var verifiedUser = await _loginService.LoginConfirm(this);

    //    if (verifiedUser == null)
    //    {
    //        // ACCESS DENIED //
    //        ErrorMessage = "Chyba přihlášení !!";
    //        Password = "";
    //        return;
    //    }

    //    // ACCESS GRANTED //
    //    Email = "";
    //    Password = "";

    //    _userStore.User = verifiedUser;
    //    _userStore.IsUserLogged = true;

    //    _regionManager.RequestNavigate(MainRegion, nameof(HomePage));
    //}


    //// INSTANT ACCESS //
    //[RelayCommand]
    //private async Task InstaAccess()
    //{
    //    var user = await _dataService.GetUser("Juzba@gmail.com");

    //    if (user == null) return;
    //    _userStore.User = user;
    //    _userStore.IsUserLogged = true;

    //    _navigation.NavigateTo<ManagerPage>();

    //}
}
