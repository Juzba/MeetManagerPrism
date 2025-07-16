using Isopoh.Cryptography.Argon2;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.ViewModels;

namespace MeetManagerPrism.Services
{

    public interface ILoginService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);

        //Task<User?> LoginConfirm(LoginPageViewModel loginVM);
    }



    public class LoginService(IDataService dataService) : ILoginService
    {
        private IDataService _dataService = dataService;

        // HASH PASSWORD with Argon2 //
        public string HashPassword(string password) => Argon2.Hash(password);


        // VERIFY PASSWORD //
        public bool VerifyPassword(string password, string hash) => Argon2.Verify(hash, password);

        // LOGIN //
        //public async Task<User?> LoginConfirm(LoginPageViewModel loginVM)
        //{
        //    var user = await _dataService.GetUser(loginVM.Email ?? "");
        //    if (user != null && VerifyPassword(loginVM.Password ?? "", user.PasswordHash)) return user;
        //    return null;
        //}

    }
}
