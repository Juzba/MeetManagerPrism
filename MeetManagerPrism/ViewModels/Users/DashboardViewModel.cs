using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace MeetManagerPrism.ViewModels.Users
{
    public class DashboardViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;
        private readonly IEventAggregator _eventAggregator;
        private DispatcherTimer _timer = new();


        private readonly AsyncDelegateCommand OnInitializeCommand;

        public DashboardViewModel(IDataService dataService, UserStore userStore, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _userStore = userStore;
            _eventAggregator = eventAggregator;
            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);

            TimerEvent();
            OnInitializeCommand.Execute();
        }

        // I-REGION-AWARE //
        public void OnNavigatedTo(NavigationContext navigationContext) { _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Dashboard"); }
        public bool IsNavigationTarget(NavigationContext navigationContext) => false;
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _timer.Tick -= Timer_Tick;
            _timer.Stop();
        }


        private async Task OnInitialize()
        {
            // Today Events
            var tEvents = await _dataService.GetTodayEventsList(_userStore.User!);
            TodayEvents = new ObservableCollection<Event>(tEvents);

            // Upcoming events
            var uEvents = await _dataService.GetUpcomingEventsList(_userStore.User!);
            UpcomingEvents = new ObservableCollection<Event>(uEvents);

            // Event invitations
            var iEvents = await _dataService.GetEventsList_byInvitedUser(_userStore.User!);
            EventInvationList = new ObservableCollection<Event>(iEvents);
        }




        // TODAY EVENTS //
        private ObservableCollection<Event> todayEvents = [];
        public ObservableCollection<Event> TodayEvents
        {
            get { return todayEvents; }
            set { SetProperty(ref todayEvents, value); }
        }

        // UPCOMING EVENTS //
        private ObservableCollection<Event> upcomingEvents = [];
        public ObservableCollection<Event> UpcomingEvents
        {
            get { return upcomingEvents; }
            set { SetProperty(ref upcomingEvents, value); }
        }

        // EVENTS INVITIONS //
        private ObservableCollection<Event> eventInvationList = [];
        public ObservableCollection<Event> EventInvationList
        {
            get { return eventInvationList; }
            set { SetProperty(ref eventInvationList, value); }
        }



        // ACTUAL TIME //
        private DateTime actualTime = DateTime.Now;
        public DateTime ActualTime
        {
            get { return actualTime; }
            set { SetProperty(ref actualTime, value); }
        }

        // ACTUAL TIME EVENT //
        private void TimerEvent()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e) { ActualTime = DateTime.Now; }

    }
}
