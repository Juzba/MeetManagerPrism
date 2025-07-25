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
    private AsyncDelegateCommand GetEventTypeListCommand { get; }
    private AsyncDelegateCommand GetRoomListCommand { get; }
    public AsyncDelegateCommand CreateEventCommand { get; }
    public AsyncDelegateCommand DeleteEventCommand { get; }


    public CreateEventViewModel(IDataService dataService, IRegionManager regionManager, UserStore userStore)
    {
        _dataService = dataService;
        _regionManager = regionManager;
        _userStore = userStore;

        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        GetEventTypeListCommand = new AsyncDelegateCommand(GetEventTypeList);
        GetRoomListCommand = new AsyncDelegateCommand(GetRoomList);
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


    // EVENT //
    private Event myEvent = new() { StartDate = DateTime.Now.AddDays(7), EndDate = DateTime.Now.AddDays(8)};
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

        MyEvent.UserId = _userStore.User!.Id;

        if (EventParametr == null) await _dataService.AddEvent(MyEvent);
        else
        {
            MyEvent.Room = null!;
            MyEvent.EventType = null!;
            await _dataService.UpdateEvent(MyEvent);
        }

        _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage));
    }

    // DELETE EVENT //
    private async Task DeleteEvent()
    {
        await _dataService.DeleteEvent(MyEvent);
        _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage));
    }

}
