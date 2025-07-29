
using DryIoc.ImTools;
using MeetManagerPrism.Common;
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views.Users;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Users;

public class EventDetailsViewModel : BindableBase, INavigationAware
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRegionManager _regionManager;
    private readonly IDataService _dataService;
    private readonly UserStore _userStore;

    private readonly AsyncDelegateCommand OnInitializeCommand;
    public DelegateCommand NavDashboardPageCommand { get; }
    public AsyncDelegateCommand AcceptCommand { get; }
    public AsyncDelegateCommand DeclineCommand { get; }

    public EventDetailsViewModel(UserStore userStore, IDataService dataService, IEventAggregator eventAggregator, IRegionManager regionManager)
    {
        _eventAggregator = eventAggregator;
        _regionManager = regionManager;
        _dataService = dataService;
        _userStore = userStore;

        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        NavDashboardPageCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.MainRegion, nameof(Dashboard)));
        AcceptCommand = new AsyncDelegateCommand(Accept);
        DeclineCommand = new AsyncDelegateCommand(Decline);

    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => false;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.ContainsKey("Event"))
            EventParametr = (Event?)navigationContext.Parameters["Event"];

        _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Event Details");

        OnInitializeCommand.Execute();
    }


    // ON INITIALIZE //
    private async Task OnInitialize()
    {
        await LoadInvitedUsers();
        GetStatus();
    }

    // LOAD INVITED USERS FROM DB //
    private async Task LoadInvitedUsers()
    {
        if (EventParametr == null) return;

        var invitedUsers = await _dataService.GetInvitedUsersList_FromEvent(EventParametr.Id);
        InvitedUsersList = new ObservableCollection<InvitedUser>(invitedUsers);
    }


    // LOGGED USER INVITATION STATUS //
    private void GetStatus()
    {
        InvitedUser = invitedUsersList.FindFirst(p => p.UserId == _userStore.User!.Id);
    }



    // EVENT PARAMETR //
    private Event? eventParametr;
    public Event? EventParametr
    {
        get { return eventParametr; }
        set { SetProperty(ref eventParametr, value); }
    }


    // INVITED USERS LIST //
    private ObservableCollection<InvitedUser> invitedUsersList = [];
    public ObservableCollection<InvitedUser> InvitedUsersList
    {
        get { return invitedUsersList; }
        set { SetProperty(ref invitedUsersList, value); }
    }


    // INVITED USER -> LOGGED USER //
    private InvitedUser? invitedUser;
    public InvitedUser? InvitedUser
    {
        get { return invitedUser; }
        set { SetProperty(ref invitedUser, value); }
    }


    // ACCEPT //
    private async Task Accept()
    {

    }

    // DECLINE //
    private async Task Decline()
    {

    }

}
