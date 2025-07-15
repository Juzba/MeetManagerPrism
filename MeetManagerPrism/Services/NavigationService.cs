/*

using CommunityToolkit.Mvvm.Messaging;

namespace MeetManagerWPF.Services
{

    public interface INavigationService 
    {
        void NavigateTo<TPage>(string frameName = Constants.FrameMainVindowView);
    }


    public class NavigationService : INavigationService
    {

        public void NavigateTo<TPage>(string frameName = Constants.FrameMainVindowView)
        {
            WeakReferenceMessenger.Default.Send(new NavigateMessage(typeof(TPage), frameName));
        }
    }
}
*/