using MeetManagerPrism.Common;
using MeetManagerPrism.Views.Manager;

namespace MeetManagerPrism.ViewModels.Manager
{
    public partial class ManagerMainViewModel : BindableBase, IRegionAware
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavCreateEventCommand { get; }
        public DelegateCommand NavSeznamCommand { get; }

        public ManagerMainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavCreateEventCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.ManagerRegion, nameof(CreateEventPage)));
            NavSeznamCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.ManagerRegion, nameof(ManagerEventsPage)));

        }

        // INAVIGATIONAWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false; // Vždy vytvoří novou instanci
        public void OnNavigatedTo(NavigationContext navigationContext) { NavSeznamCommand.Execute(); }
        public void OnNavigatedFrom(NavigationContext navigationContext) { _regionManager.Regions.Remove("ManagerRegion"); }



    }
}
