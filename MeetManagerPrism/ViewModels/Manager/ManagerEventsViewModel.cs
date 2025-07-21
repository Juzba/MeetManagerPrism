using MeetManagerPrism.Common;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using MeetManagerPrism.Views.Manager;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Manager
{
    public class ManagerEventsViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;
        private readonly IRegionManager _regionManager;
        private AsyncDelegateCommand OnInitializeCommand;

        public ManagerEventsViewModel(IDataService dataService, UserStore userStore, IRegionManager regionManager)
        {
            _dataService = dataService;
            _userStore = userStore;
            _regionManager = regionManager;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);

            OnInitializeCommand.Execute();
        }


        // INAVIGATIONAWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext) { }
        public void OnNavigatedFrom(NavigationContext navigationContext) { }


        // ON INITIALIZE //
        private async Task OnInitialize()
        {
            var events = await _dataService.GetEventsList(_userStore.User!);
            EventsList = new ObservableCollection<Event>(events);
        }


        // EVENT LIST //
        private ObservableCollection<Event> eventsList = [];
        public ObservableCollection<Event> EventsList
        {
            get { return eventsList; }
            set { SetProperty(ref eventsList, value); }
        }


        // SELECTED EVENT PROPERTY //
        private Event? selectedEvent;
        public Event? SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                SetProperty(ref selectedEvent, value);
                OnSelect(value);
            }
        }


        // ON SELECT ACTION //
        private void OnSelect(Event? selectedEvent)
        {
            if (selectedEvent == null) return;


            var parametr = new NavigationParameters();
            parametr.Add("Event", selectedEvent);
            _regionManager.RequestNavigate(Const.ManagerRegion, nameof(CreateEventPage), parametr);
        }






    }
}
