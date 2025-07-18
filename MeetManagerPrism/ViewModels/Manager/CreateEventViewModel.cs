using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Manager;

public partial class CreateEventViewModel : BindableBase
{
    private readonly IDataService _dataService;



    public CreateEventViewModel(IDataService dataService)
    {
        _dataService = dataService;
        //GetEventTypeListCommand.Execute(null);
        //GetRoomListCommand.Execute(null);
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

    // SELECTED EVENT-TYPE //
    private EventType? selectedEventType;
    public EventType? SelectedEventType
    {
        get { return selectedEventType; }
        set { SetProperty(ref selectedEventType, value); }
    }


    // ROOMS LIST //
    private ObservableCollection<Room> roomList;
    public ObservableCollection<Room> RoomList
    {
        get { return roomList; }
        set { SetProperty(ref roomList, value); }
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


    [ObservableProperty]
    private DateTime startEvent;

    [ObservableProperty]
    private DateTime endEvent;

    [ObservableProperty]
    private string? description;




    [RelayCommand]
    public async Task CreateEvent()
    {
        if (Name == null || Description == null || SelectedEventType == null)
        {
            ErrorMessage = "Chybí údaje!";
            return;
        }

        var newEvent = new Event() { Name = Name, Description = Description, EndDate = EndEvent, StartDate = StartEvent, EventTypeId = SelectedEventType.Id, RoomID = 0 };
        await _dataService.AddEvent(newEvent);

    }


    [RelayCommand]
    public void DeleteEvent()
    {

    }

}
