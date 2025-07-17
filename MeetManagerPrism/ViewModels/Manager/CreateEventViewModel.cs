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



    //// COMBOBOX EVENT TYPE //
    //[RelayCommand]
    //private async Task GetEventTypeList()
    //{
    //    var data = await _dataService.GetEventTypeList();
    //    EventTypeList = new ObservableCollection<EventType>(data);
    //}

    //[ObservableProperty]
    //private ObservableCollection<EventType> eventTypeList = [];

    //[ObservableProperty]
    //private EventType? selectedEventType;



    //// COMBOBOX ROOM //
    //[RelayCommand]
    //private async Task GetRoomList()
    //{
    //    var data = await _dataService.GetRoomList();
    //    RoomList = new ObservableCollection<Room>(data);
    //}

    //[ObservableProperty]
    //private ObservableCollection<Room> roomList = [];

    //[ObservableProperty]
    //private EventType? selectedRoom;






    //[ObservableProperty]
    //private string? errorMessage;

    //[ObservableProperty]
    //private string? name;

    //[ObservableProperty]
    //private DateTime startEvent;

    //[ObservableProperty]
    //private DateTime endEvent;

    //[ObservableProperty]
    //private string? description;




    //[RelayCommand]
    //public async Task CreateEvent()
    //{
    //    if (Name == null || Description == null || SelectedEventType == null)
    //    {
    //        ErrorMessage = "Chybí údaje!";
    //        return;
    //    }

    //    var newEvent = new Event() { Name = Name, Description = Description, EndDate = EndEvent, StartDate = StartEvent, EventTypeId = SelectedEventType.Id, RoomID = 0 };
    //    await _dataService.AddEvent(newEvent);

    //}


    //[RelayCommand]
    //public void DeleteEvent()
    //{

    //}

}
