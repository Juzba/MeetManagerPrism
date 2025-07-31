using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin;

public class AdminRoomsViewModel : BindableBase, IRegionAware
{
    private readonly IDataService _dataService;

    private AsyncDelegateCommand OnInitializeCommand;
    public AsyncDelegateCommand SaveChangesCommand { get; }
    public AsyncDelegateCommand AddNewRoomCommand { get; }
    public AsyncDelegateCommand<object?> RemoveRoomCommand { get; }


    public AdminRoomsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
        SaveChangesCommand = new AsyncDelegateCommand(SaveChanges);
        RemoveRoomCommand = new AsyncDelegateCommand<object?>(RemoveRoom);
        AddNewRoomCommand = new AsyncDelegateCommand(AddNewRoom);


        OnInitializeCommand.Execute();
    }


    // I-REGION-AWARE //
    public bool IsNavigationTarget(NavigationContext navigationContext) => false;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
    public void OnNavigatedTo(NavigationContext navigationContext) { }


    // ON INITIALIZE //
    private async Task OnInitialize()
    {
        await LoadRoomsList();
    }


    // LOAD ROOMS FROM DB //
    private async Task LoadRoomsList()
    {
        var roomsData = await _dataService.GetRoomList();
        RoomList = new ObservableCollection<Room>(roomsData);
    }


    // ROOMS LIST //
    private ObservableCollection<Room> roomList = [];
    public ObservableCollection<Room> RoomList
    {
        get { return roomList; }
        set { SetProperty(ref roomList, value); }
    }


    // NEW ROOM //
    private Room newRoom = new();
    public Room NewRoom
    {
        get { return newRoom; }
        set { SetProperty(ref newRoom, value); }
    }


    // ERROR MESSAGE //
    private string? errorMessage;
    public string? ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }



    // SAVE CHANGES //
    private async Task SaveChanges()
    {
        await _dataService.SaveChanges();
    }


    // REMOVE ROOM //
    private async Task RemoveRoom(object? param)
    {
        if (param is not Room room) return;

        await _dataService.DeleteRoom(room);
        await _dataService.SaveChanges();
        await LoadRoomsList();
    }

    // ADD ROOM //
    private async Task AddNewRoom()
    {
        if (string.IsNullOrWhiteSpace(NewRoom.Name) || string.IsNullOrWhiteSpace(NewRoom.Location) || NewRoom.Capacity == 0)
        {
            ErrorMessage = "Chybí hodnoty pro přidání pokoje.";
            return;
        }

        await _dataService.AddRoom(NewRoom);
        await _dataService.SaveChanges();

        NewRoom = new();
        ErrorMessage = null;
        await OnInitialize();
    }

}
