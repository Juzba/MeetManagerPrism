using MeetManagerPrism.Common;
using MeetManagerPrism.Common.Events;
using MeetManagerPrism.Views.Manager;
using System.Drawing;

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

            NavCreateEventCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.ManagerRegion, nameof(CreateEventPage)));
            NavSeznamCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage)));
        }

        // INAVIGATIONAWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext) 
        {
            NavSeznamCommand.Execute(); 
            _eventAggregator.GetEvent<MainViewTitleEvent>().Publish("Event Manager");
        }
        public void OnNavigatedFrom(NavigationContext navigationContext) { _regionManager.Regions.Remove("ManagerRegion"); }



    }
}
