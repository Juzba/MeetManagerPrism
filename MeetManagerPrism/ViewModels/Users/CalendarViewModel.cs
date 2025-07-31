
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Users
{
    public class CalendarViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;

        private readonly AsyncDelegateCommand OnInitializeCommand;



        public CalendarViewModel(UserStore userStore, IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _userStore = userStore;

            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);

            OnInitializeCommand.Execute();
        }



        // I-NAVIGATION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false;
        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Calendar");
        }


        // ON INITIALIZE //
        private async Task OnInitialize()
        {
            // load events from db
            var events = await _dataService.GetAceptedEventsList_byInvitedUser(_userStore.User!);
            EventList = new ObservableCollection<Event>(events);
        }


        // EVENT LIST //
        private ObservableCollection<Event> eventList = [];
        public ObservableCollection<Event> EventList
        {
            get { return eventList; }
            set { SetProperty(ref eventList, value); }
        }


    }
}
