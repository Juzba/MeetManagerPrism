using MeetManagerPrism.Data.Model;

namespace MeetManagerPrism.Services
{
    public class UserStore : BindableBase
    {
        // IS USER LOGGED? //
        private bool _isUserLogged;
        public bool IsUserLogged
        {
            get { return _isUserLogged; }
            set { SetProperty(ref _isUserLogged, value); }
        }


        // USER STORAGE //
        private User? _user;
        public User? User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
    }
}
