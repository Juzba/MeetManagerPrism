
using MeetManagerPrism.Common.Events;

namespace MeetManagerPrism.ViewModels.Users
{
    public class CalendarViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;

        public CalendarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }


        // I-NAVIGATION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false;
        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        public void OnNavigatedTo(NavigationContext navigationContext) 
        {
            _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Calendar");
        }





    }
}
