using MeetManagerPrism.Common;
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Views.Manager;

namespace MeetManagerPrism.ViewModels.Manager
{
    public partial class ManagerMainViewModel : BindableBase, IRegionAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand NavCreateEventCommand { get; }
        public DelegateCommand NavSeznamCommand { get; }

        public ManagerMainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavCreateEventCommand = new DelegateCommand(() => { _regionManager.RequestNavigate(Const.ManagerRegion, nameof(CreateEventPage)); CurrentPage = "CreateEvent"; });
            NavSeznamCommand = new DelegateCommand(() => { _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage)); CurrentPage = "Events"; });
        }

        // I-NAVIGATION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavSeznamCommand.Execute();
            _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Event Manager");
        }
        public void OnNavigatedFrom(NavigationContext navigationContext) { _regionManager.Regions.Remove("ManagerRegion"); }


        // CURRENT PAGE //
        private string currentPage = "";
        public string CurrentPage
        {
            get { return currentPage; }
            set { SetProperty(ref currentPage, value); }
        }




    }
}
