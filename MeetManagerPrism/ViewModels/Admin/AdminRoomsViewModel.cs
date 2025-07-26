using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Admin
{
    public class AdminRoomsViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;

        private AsyncDelegateCommand OnInitializeCommand;
        public AsyncDelegateCommand SaveChangesCommand { get; }
        public AsyncDelegateCommand<object?> RemoveRoomCommand { get; }


        public AdminRoomsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);
            SaveChangesCommand = new AsyncDelegateCommand(SaveChanges);
            RemoveRoomCommand = new AsyncDelegateCommand<object?>(RemoveRoom);

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

        // SAVE CHANGES //
        private async Task SaveChanges() => await _dataService.SaveChangesDB();

        // REMOVE ROOM //
        private async Task RemoveRoom(object? param)
        {
            if (param is not Room room) return;

            await _dataService.DeleteRoom(room);
            await LoadRoomsList();


        }
    }
}
