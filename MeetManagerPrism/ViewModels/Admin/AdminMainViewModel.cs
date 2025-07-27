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

            NavAdminUsersCommand = new DelegateCommand(() => { _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminUsersPage)); CurrentPage = "Users"; });
            NavAdminRoomsCommand = new DelegateCommand(() => { _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminRoomsPage)); CurrentPage = "Rooms"; });
            NavAdminEventTypesCommand = new DelegateCommand(() => { _regionManager.RequestNavigate(Const.AdminRegion, nameof(AdminEventTypesPage)); CurrentPage = "EventTypes"; });
        }




        // I-REGION-AWARE //
        public bool IsNavigationTarget(NavigationContext navigationContext) => false;
        public void OnNavigatedFrom(NavigationContext navigationContext) => _regionManager.Regions.Remove("AdminRegion");
        public void OnNavigatedTo(NavigationContext navigationContext) => NavAdminUsersCommand.Execute();




        // CURRENT PAGE //

        private string currentPage = "";
        public string CurrentPage
        {
            get { return currentPage; }
            set { SetProperty(ref currentPage, value); }
        }


    }
}
