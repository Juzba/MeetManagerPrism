using MeetManagerPrism.Common;
using MeetManagerPrism.Views.Admin;

namespace MeetManagerPrism.ViewModels.Admin
{
    public class AdminMainViewModel : BindableBase, IRegionAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand NavAdminUsersCommand { get; }


        public AdminMainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavAdminUsersCommand = new DelegateCommand(() => _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminUsersPage)));
        }




        // I-REGION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        public void OnNavigatedTo(NavigationContext navigationContext) { _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminUsersPage)); }



    }
}
