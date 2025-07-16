using Isopoh.Cryptography.Argon2;

namespace MeetManagerPrism.Services;



public interface IHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}



public class HashService : IHashService
{
    // HASH PASSWORD with Argon2 //
    public string HashPassword(string password) => Argon2.Hash(password);


    // VERIFY PASSWORD //
    public bool VerifyPassword(string password, string hash) => Argon2.Verify(hash, password);


};