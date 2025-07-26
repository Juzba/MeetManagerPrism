using MeetManagerPrism.Common;
using MeetManagerPrism.Views.Admin;

namespace MeetManagerPrism.ViewModels.Admin
{
    public class AdminMainViewModel : BindableBase, IRegionAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand NavAdminUsersCommand { get; }
        public DelegateCommand NavAdminRoomsCommand { get; }
        public DelegateCommand NavAdminEventTypesCommand { get; }


        public AdminMainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavAdminUsersCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminUsersPage)));
            NavAdminRoomsCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminRoomsPage)));
            NavAdminEventTypesCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminEventTypesPage)));
        }




        // I-REGION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        public void OnNavigatedTo(NavigationContext navigationContext) { _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminUsersPage)); }



    }
}
