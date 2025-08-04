using MeetManagerPrism.Common;
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views;


namespace MeetManagerPrism.ViewModels;

public partial class RegisterViewModel : BindableBase, IRegionAware
{
    private readonly ILoginService _loginService;
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    public AsyncDelegateCommand RegisterCommand { get; }


    public RegisterViewModel(ILoginService loginService, IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _loginService = loginService;
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        RegisterCommand = new AsyncDelegateCommand(Register);
    }

    // I-NAVIGATION-AWARE //
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
    public void OnNavigatedTo(NavigationContext navigationContext) { _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Register"); }
    public bool IsNavigationTarget(NavigationContext navigationContext) => false;



    // EMAIL //
    private string email = "";
    public string Email
    {
        get { return email; }
        set { SetProperty(ref email, value); }
    }

    // PASSWORD //
    private string passwordA = "";
    public string PasswordA
    {
        get { return passwordA; }
        set { SetProperty(ref passwordA, value); }
    }


    // PASSWORD2 //
    private string passwordB = "";
    public string PasswordB
    {
        get { return passwordB; }
        set { SetProperty(ref passwordB, value); }
    }


    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }




    // REGISTER COMMAND //
    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrEmpty(PasswordA) || !PasswordA.Equals(PasswordB))
        {
            ErrorMessage = "Chybí email nebo obě hesla nejsou stejná.";
            return;
        }

        if (await _loginService.TryRegister(Email, PasswordA))
        {
            // REGISTER SUCCESS //
            _regionManager.RequestNavigate(Const.MainRegion, nameof(LoginPage));
            return;
        }

        ErrorMessage = "Uživatel s tímto emailem již existuje.";
    }
}






