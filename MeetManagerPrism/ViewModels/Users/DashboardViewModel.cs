using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using System.Collections.ObjectModel;

namespace MeetManagerPrism.ViewModels.Users
{
    public class DashboardViewModel : BindableBase, IRegionAware
    {
        private readonly IDataService _dataService;
        private readonly UserStore _userStore;

        private readonly AsyncDelegateCommand OnInitializeCommand;

        public DashboardViewModel(IDataService dataService, UserStore userStore)
        {
            _dataService = dataService;
            _userStore = userStore;
            OnInitializeCommand = new AsyncDelegateCommand(OnInitialize);

            OnInitializeCommand.Execute();
        }

        // I-REGION-AWARE //
        public void OnNavigatedTo(NavigationContext navigationContext) { }
        public bool IsNavigationTarget(NavigationContext navigationContext) => false;
        public void OnNavigatedFrom(NavigationContext navigationContext) { }


        private async Task OnInitialize()
        {
            var tEvents = await _dataService.GetTodayEventsList(_userStore.User!);
            TodayEvents = new ObservableCollection<Event>(tEvents);

            var uEvents = await _dataService.GetUpcomingEventsList(_userStore.User!);
            UpcomingEvents = new ObservableCollection<Event>(uEvents);
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



        // DATE_TIME //
        private DateTime dateTime = DateTime.Now;
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

    }
}
