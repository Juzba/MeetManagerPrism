using MeetManagerPrism.Common;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views;

namespace MeetManagerPrism.ViewModels;

public partial class LoginViewModel : BindableBase, INavigationAware
{
    private readonly IDataService _dataService;
    private readonly UserStore _userStore;
    private readonly ILoginService _loginService;
    private readonly IRegionManager _regionManager;

    public AsyncDelegateCommand LoginCommand { get; }
    public AsyncDelegateCommand InstaAccessCommand { get; }


    // INAVIGATIONAWARE //
    public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public void OnNavigatedFrom(NavigationContext navigationContext) { }


    public LoginViewModel(IDataService dataService, UserStore userStore, ILoginService loginService, IRegionManager regionManager)
    {
        _dataService = dataService;
        _userStore = userStore;
        _loginService = loginService;
        _regionManager = regionManager;

        LoginCommand = new AsyncDelegateCommand(Login);
        InstaAccessCommand = new AsyncDelegateCommand(InstaAccess);
    }




    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }


    // EMAIL //
    private string? email;
    public string? Email
    {
        get { return email; }
        set { SetProperty(ref email, value); }
    }

    // PASSWORD //
    private string? password;
    public string? Password
    {
        get { return password; }
        set { SetProperty(ref password, value); }
    }


    // LOGIN COMMAND //
    private async Task Login()
    {
        ErrorMessage = "";

        if (await _loginService.TryLogin(this))
        {
            // ACCESS GRANTED //
            _regionManager.RequestNavigate(Const.MainRegion, nameof(HomePage));
            return;
        }

        // ACCESS DENIED //
        ErrorMessage = "Chyba přihlášení !!";
        Password = "";
    }


    // INSTANT ACCESS //
    private async Task InstaAccess()
    {
        if (await _loginService.TryInstaAccess())
            _regionManager.RequestNavigate(Const.MainRegion, nameof(HomePage));
    }
}
