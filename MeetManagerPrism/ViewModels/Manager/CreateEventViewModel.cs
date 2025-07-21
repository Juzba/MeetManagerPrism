using MeetManagerPrism.Common;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views.Manager;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Manager;

public partial class CreateEventViewModel : BindableBase, IRegionAware, INavigationAware
{
    private readonly IDataService _dataService;
    private readonly IRegionManager _regionManager;
    private readonly UserStore _userStore;

    private AsyncDelegateCommand OnInitializeCommand { get; }
    private AsyncDelegateCommand GetEventTypeListCommand { get; }
    private AsyncDelegateCommand GetRoomListCommand { get; }
    public AsyncDelegateCommand CreateEventCommand { get; }
    public DelegateCommand DeleteEventCommand { get; }


    public CreateEventViewModel(IDataService dataService, IRegionManager regionManager, UserStore userStore)
    {
        _dataService = dataService;
        _regionManager = regionManager;
        _userStore = userStore;

        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        GetEventTypeListCommand = new AsyncDelegateCommand(GetEventTypeList);
        GetRoomListCommand = new AsyncDelegateCommand(GetRoomList);
        CreateEventCommand = new AsyncDelegateCommand(CreateEvent);
        DeleteEventCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.ManagerRegion, nameof(CreateEventPage)));

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

            Name = EventParametr.Name;
            StartEvent = EventParametr.StartDate;
            EndEvent = EventParametr.EndDate;
            Description = EventParametr.Description;
            SelectedEventType = EventParametr.EventType;
            SelectedRoom = EventParametr.Room;

        }
    }
    public bool IsNavigationTarget(NavigationContext navigationContext) => false;




    // ON INITIALIZE //
    private async Task OnInitialize()
    {
        await GetEventTypeListCommand.Execute();
        await GetRoomListCommand.Execute();
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


    // SELECTED EVENT-TYPE //
    private EventType? selectedEventType;
    public EventType? SelectedEventType
    {
        get { return selectedEventType; }
        set { SetProperty(ref selectedEventType, value); }
    }


    // SELECTED ROOM //
    private Room? selectedRoom;
    public Room? SelectedRoom
    {
        get { return selectedRoom; }
        set { SetProperty(ref selectedRoom, value); }
    }


    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }


    // NAME //
    private string? name;
    public string? Name
    {
        get { return name; }
        set { SetProperty(ref name, value); }
    }


    // START EVENT //
    private DateTime startEvent = DateTime.Now.AddDays(7);
    public DateTime StartEvent
    {
        get { return startEvent; }
        set { SetProperty(ref startEvent, value); }
    }


    // END EVENT //
    private DateTime endEvent = DateTime.Now.AddDays(8);
    public DateTime EndEvent
    {
        get { return endEvent; }
        set { SetProperty(ref endEvent, value); }
    }


    // DESCRIPTION //
    private string? description;
    public string? Description
    {
        get { return description; }
        set { SetProperty(ref description, value); }
    }

    // INPUT PARAMETR SELECTED EVENT //
    private Event? eventParametr;
    public Event? EventParametr
    {
        get { return eventParametr; }
        set { SetProperty(ref eventParametr, value); }
    }




    // CREATE EVENT //
    private async Task CreateEvent()
    {
        if (Name == null || Description == null || SelectedEventType == null || SelectedRoom == null)
        {
            ErrorMessage = "Chybí údaje!";
            return;
        }

        var newEvent = new Event()
        {
            Name = Name,
            Description = Description,
            EndDate = EndEvent,
            StartDate = StartEvent,
            EventTypeId = SelectedEventType.Id,
            RoomID = SelectedRoom.ID,
            UserId = _userStore.User!.Id
        };
        await _dataService.AddEvent(newEvent);

        _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage));
    }

}
