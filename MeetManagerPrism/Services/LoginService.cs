using Isopoh.Cryptography.Argon2;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.ViewModels;

namespace MeetManagerPrism.Services
{

    public interface ILoginService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);

        Task<bool> LoginConfirm(LoginViewModel loginVM);
        Task<bool> InstaAccess();
    }



    public class LoginService(IDataService dataService, UserStore userStore) : ILoginService
    {
        private IDataService _dataService = dataService;
        private UserStore _userStore = userStore;

        // HASH PASSWORD with Argon2 //
        public string HashPassword(string password) => Argon2.Hash(password);


        // VERIFY PASSWORD //
        public bool VerifyPassword(string password, string hash) => Argon2.Verify(hash, password);

        // LOGIN //
        public async Task<bool> LoginConfirm(LoginViewModel loginVM)
        {
            var user = await _dataService.GetUser(loginVM.Email ?? "");
            if (user == null || !VerifyPassword(loginVM.Password ?? "", user.PasswordHash)) return false;

            // Save logged user //
            _userStore.User = user;
            _userStore.IsUserLogged = true;

            return true;
        }

        // INSTANT ACCESS //

        public async Task<bool> InstaAccess()
        {
            var user = await _dataService.GetUser("Juzba@gmail.com");

            if (user == null) return false;

            _userStore.User = user;
            _userStore.IsUserLogged = true;
            return true;     
        }



    }
}
