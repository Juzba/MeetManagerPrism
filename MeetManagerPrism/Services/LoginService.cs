using Isopoh.Cryptography.Argon2;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.ViewModels;

namespace MeetManagerPrism.Services
{

    public interface ILoginService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);

        Task<bool> TryLogin(string email, string password);
        Task<bool> TryInstaAccess();
        Task<bool> TryRegister(string email, string password);
    }



    public class LoginService(IDataService dataService, UserStore userStore) : ILoginService
    {
        private IDataService _dataService = dataService;
        private UserStore _userStore = userStore;

        // HASH PASSWORD with Argon2 //
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "password is null or empty!");

            return Argon2.Hash(password);
        }


        // VERIFY PASSWORD //
        public bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "password is null or empty!");
            if (string.IsNullOrWhiteSpace(hash)) throw new ArgumentNullException(nameof(hash), "hash is null or empty!");

            return Argon2.Verify(hash, password);
        }


        // LOGIN //
        public async Task<bool> TryLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email), "email is null or empty!");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "password is null or empty!");

            var user = await _dataService.GetUser(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash)) return false;

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
        public async Task<bool> TryRegister(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email), "email is null or empty!");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "password is null or empty!");

            // user is in db?
            if (await _dataService.GetUser(email) != null) return false;

            var hash = HashPassword(password);
            var newUser = new User() { Name = email, Email = email, PasswordHash = hash };

            await _dataService.AddUser(newUser);
            await _dataService.SaveChanges();

            return true;
        }

    }
}
