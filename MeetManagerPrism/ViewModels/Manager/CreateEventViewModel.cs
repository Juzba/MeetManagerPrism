using MeetManagerPrism.Common;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views.Manager;
using System.Collections.ObjectModel;
using System.Windows;

namespace MeetManagerPrism.ViewModels.Manager;

public partial class CreateEventViewModel : BindableBase, IRegionAware, INavigationAware
{
    private readonly IDataService _dataService;
    private readonly IRegionManager _regionManager;
    private readonly UserStore _userStore;

    private AsyncDelegateCommand OnInitializeCommand { get; }
    public AsyncDelegateCommand CreateEventCommand { get; }
    public AsyncDelegateCommand DeleteEventCommand { get; }


    public CreateEventViewModel(IDataService dataService, IRegionManager regionManager, UserStore userStore)
    {
        _dataService = dataService;
        _regionManager = regionManager;
        _userStore = userStore;

        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        CreateEventCommand = new AsyncDelegateCommand(CreateEvent);
        DeleteEventCommand = new AsyncDelegateCommand(DeleteEvent);

        OnInitializeCommand.Execute();
    }


    // I-NAVIGATION-AWARE //
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.ContainsKey("Event"))
        {
            EventParametr = (Event?)navigationContext.Parameters["Event"];
            if (EventParametr == null) return;

            MyEvent = EventParametr;
            VisibilityDelete = Visibility.Visible;
        }
    }
    public bool IsNavigationTarget(NavigationContext navigationContext) => false;



    // ON INITIALIZE //
    private async Task OnInitialize()
    {
        await GetEventTypeList();
        await GetRoomList();
        await GetUsersList();
    }


    // LOAD EVENT-TYPES FROM DB - COMBOBOX  //
    private async Task GetEventTypeList()
    {
        var eventTypes = await _dataService.GetEventTypeList();
        EventTypeList = new ObservableCollection<EventType>(eventTypes);
    }


    // LOAD ROOMS FROM DB - COMBOBOX //
    private async Task GetRoomList()
    {
        var rooms = await _dataService.GetRoomList();
        RoomList = new ObservableCollection<Room>(rooms);
    }


    // LOAD USERS FROM DB - INVATION //
    private async Task GetUsersList()
    {
        // get users from invited-users on My-event id. 
        var invitedUsers = await _dataService.GetInvitedUsersList_FromEvent(MyEvent.Id);          // include users

        var allUsers = await _dataService.GetUsersList();                              // get all users

        foreach (User user in allUsers)                                                // if user is not invited then displey user in invitation datagrid table
                                                                                       // Not invited users
            if (!invitedUsers.Any(p => p.User == user)) UserList.Add(new InvitedUser() { User = user, Status = InvStatus.Pending });

        InvitedUsersList = new ObservableCollection<InvitedUser>(invitedUsers);         // show invited users in datagrid table
    }


    // EVENT //
    private Event myEvent = new() { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(8) };
    public Event MyEvent
    {
        get { return myEvent; }
        set { SetProperty(ref myEvent, value); }
    }


    // PARAMETR EVENT //
    private Event? eventParametr;
    public Event? EventParametr
    {
        get { return eventParametr; }
        set { SetProperty(ref eventParametr, value); }
    }


    // EVENT-TYPE LIST //
    private ObservableCollection<EventType> eventTypeList = [];
    public ObservableCollection<EventType> EventTypeList
    {
        get { return eventTypeList; }
        set { SetProperty(ref eventTypeList, value); }
    }


    // ROOMS LIST //
    private ObservableCollection<Room> roomList = [];
    public ObservableCollection<Room> RoomList
    {
        get { return roomList; }
        set { SetProperty(ref roomList, value); }
    }


    // USERS LIST //
    private ObservableCollection<InvitedUser> userList = [];
    public ObservableCollection<InvitedUser> UserList
    {
        get { return userList; }
        set { SetProperty(ref userList, value); }
    }

    // SELECTED USER //
    private InvitedUser? selectedUser;
    public InvitedUser? SelectedUser
    {
        get { return selectedUser; }
        set
        {
            selectedUser = value;
            SelectedUserFunc();
        }
    }

    // SELECTED INVITED USER //
    private InvitedUser? selectedInvitedUser;
    public InvitedUser? SelectedInvitedUser
    {
        get { return selectedInvitedUser; }
        set
        {
            selectedInvitedUser = value;
            SelectedInvitedUserFunc();
        }
    }



    // INVITED USERS LIST - PARTICIPANTS //
    private ObservableCollection<InvitedUser> invitedUsersList = [];
    public ObservableCollection<InvitedUser> InvitedUsersList
    {
        get { return invitedUsersList; }
        set { SetProperty(ref invitedUsersList, value); }
    }



    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }

    // DELETE VISIBILITY //

    private Visibility visibilityDelete = Visibility.Collapsed;
    public Visibility VisibilityDelete
    {
        get { return visibilityDelete; }
        set { SetProperty(ref visibilityDelete, value); }
    }


    // CREATE EVENT //
    private async Task CreateEvent()
    {
        if (MyEvent.Name == null || MyEvent.Description == null || MyEvent.RoomID == 0 || MyEvent.EventTypeId == 0)
        {
            ErrorMessage = "Chybí údaje!";
            return;
        }

        if (EventParametr == null)
        {
            // NEW EVENT
            MyEvent.AutorId = _userStore.User!.Id;       // logged user id
            await _dataService.AddEvent(MyEvent);

            var invitation = new Invitation()
            {
                Event = MyEvent,
                SentDate = DateTime.Now,
                AutorId = _userStore.User!.Id,
                InvitedUsers = InvitedUsersList
            };
            await _dataService.AddInvitation(invitation);
        }
        else
        {
            // EDIT EVENT
            MyEvent.Room = null!;
            MyEvent.EventType = null!;
            await _dataService.UpdateEvent(MyEvent);



            // EDIT INVITATION
            var invitation = await _dataService.GetInvitation(MyEvent);

            if (invitation == null)
            {
                ErrorMessage = "Invitation se nenalezl!";
                return;
            }

            invitation.InvitedUsers = invitedUsersList;
            await _dataService.UpdateInvitation(invitation);

        }

        _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage));
    }

    // DELETE EVENT //
    private async Task DeleteEvent()
    {
        await _dataService.DeleteEvent(MyEvent);
        _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage));
    }


    // SELECTED USER //
    private void SelectedUserFunc()
    {
        if (SelectedUser == null || InvitedUsersList.Contains(SelectedUser)) return;

        InvitedUsersList.Add(SelectedUser);
        UserList.Remove(SelectedUser);
    }

    // SELECTED INVITED USER //
    private void SelectedInvitedUserFunc()
    {
        if (SelectedInvitedUser == null || UserList.Contains(SelectedInvitedUser)) return;

        UserList.Add(SelectedInvitedUser);
        InvitedUsersList.Remove(SelectedInvitedUser);
    }



}
