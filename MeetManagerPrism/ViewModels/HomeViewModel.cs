using MeetManagerPrism.Common.Events;

namespace MeetManagerPrism.ViewModels
{
    public partial class HomeViewModel : BindableBase, IRegionAware
    {
        private readonly IEventAggregator _eventAggregator;

        public HomeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }


        // I-REGION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) { return false; }
        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        public void OnNavigatedTo(NavigationContext navigationContext) { _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Home"); }
    }
}
