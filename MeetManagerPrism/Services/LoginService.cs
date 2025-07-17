using Isopoh.Cryptography.Argon2;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.ViewModels;

namespace MeetManagerPrism.Services
{

    public interface ILoginService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);

        Task<bool> TryLogin(LoginViewModel loginVM);
        Task<bool> TryInstaAccess();
        Task<bool> TryRegister(RegisterViewModel registerVM);
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
        public async Task<bool> TryLogin(LoginViewModel loginVM)
        {
            var user = await _dataService.GetUser(loginVM.Email ?? "");
            if (user == null || !VerifyPassword(loginVM.Password ?? "", user.PasswordHash)) return false;

            // Save logged user //
            _userStore.User = user;
            _userStore.IsUserLogged = true;

            return true;
        }

        // INSTANT ACCESS //

        public async Task<bool> TryInstaAccess()
        {
            var user = await _dataService.GetUser("Juzba@gmail.com");

            if (user == null) return false;

            _userStore.User = user;
            _userStore.IsUserLogged = true;
            return true;
        }

        // REGISTER //

        public async Task<bool> TryRegister(RegisterViewModel registerVM)
        {
            if (await _dataService.GetUser(registerVM.Email) != null) return false;

            var hash = HashPassword(registerVM.PasswordA);
            var newUser = new User() { Name = registerVM.Email, Email = registerVM.Email, PasswordHash = hash };

            await _dataService.AddUser(newUser);

            return true;
        }

    }
}
