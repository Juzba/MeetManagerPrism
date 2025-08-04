using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Tests.Services;

public class LoginServiceTests
{
    private LoginService _loginService;
    private AppDbContext _context;
    private UserStore _userStore;

    [SetUp]
    public void SetUp()
    {

        var option = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DbTests")
            .Options;
        _context = new AppDbContext(option);

        var dataService = new DataService(_context);
         _userStore = new UserStore();
        _loginService = new LoginService(dataService, _userStore);

    }


    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();

        _userStore = null!;
    }




    [Test]
    public void HashPassword_and_VerifyPassword_Test()
    {
        var hashedPassword = _loginService.HashPassword("123456");

        var result = _loginService.VerifyPassword("123456", hashedPassword);

        Assert.That(result, Is.True);
        Assert.That(hashedPassword, Has.Length.GreaterThan(20));
        Assert.That(hashedPassword, Does.Contain("argon"));
    }


    [Test]
    public void HashPassword_and_VerifyPassword_FailTest()
    {
        string password = "123456";

        var hashedPassword = _loginService.HashPassword(password);

        var result = _loginService.VerifyPassword("456789", hashedPassword);

        Assert.That(result, Is.False);
        Assert.That(hashedPassword, Has.Length.GreaterThan(20));
        Assert.That(hashedPassword, Does.Contain("argon"));
    }


    [Test]
    public void HashPassword_InputNull_Test()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _loginService.HashPassword(null!));

        Assert.That(exception.ParamName, Is.EqualTo("password"));
        Assert.That(exception.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }


    [Test]
    public void HashPassword_InputEmpty_Test()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _loginService.HashPassword(" "));

        Assert.That(exception.ParamName, Is.EqualTo("password"));
        Assert.That(exception.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }


    [Test]
    public void VerifyPassword_InputNull_Test()
    {
        var exception1 = Assert.Throws<ArgumentNullException>(() => _loginService.VerifyPassword("password", null!));
        var exception2 = Assert.Throws<ArgumentNullException>(() => _loginService.VerifyPassword(null!, "hashPassword"));

        Assert.That(exception1.ParamName, Is.EqualTo("hash"));
        Assert.That(exception1.Message, Is.EqualTo("hash is null or empty! (Parameter 'hash')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }

    [Test]
    public void VerifyPassword_InputEmpty_Test()
    {
        var exception1 = Assert.Throws<ArgumentNullException>(() => _loginService.VerifyPassword("password", " "));
        var exception2 = Assert.Throws<ArgumentNullException>(() => _loginService.VerifyPassword(" ", "hashPassword"));

        Assert.That(exception1.ParamName, Is.EqualTo("hash"));
        Assert.That(exception1.Message, Is.EqualTo("hash is null or empty! (Parameter 'hash')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }


    [Test]
    public async Task TryLogin_Should_Return_TrueOrFalse_Test()
    {

        // add user to db
        string hashPassword = _loginService.HashPassword("123456");
        User user = new() { Id = 1, Name = "Veronika", Email = "Test@gmail.com", PasswordHash = hashPassword, Role = new() };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();


        var result1 = await _loginService.TryLogin("Test@gmail.com", "123456");
        var result2 = await _loginService.TryLogin("Test@gmail.com", "123");
        var result3 = await _loginService.TryLogin("FalseTest@gmail.com", "123456");

        var result4 = _userStore.IsUserLogged;
        var result5 = _userStore.User?.Name;


        Assert.That(result1, Is.True);
        Assert.That(result2, Is.False);
        Assert.That(result3, Is.False);
        Assert.That(result4, Is.True);
        Assert.That(result5, Is.EqualTo("Veronika"));
    }


    [Test]
    public void TryLogin_InputNull_Test()
    {
        var exception1 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryLogin(null!, "password"));
        var exception2 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryLogin("email", null! ));

        Assert.That(exception1.ParamName, Is.EqualTo("email"));
        Assert.That(exception1.Message, Is.EqualTo("email is null or empty! (Parameter 'email')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }


    [Test]
    public void TryLogin_InputEmpty_Test()
    {
        var exception1 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryLogin(" ", "password"));
        var exception2 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryLogin("email", " " ));

        Assert.That(exception1.ParamName, Is.EqualTo("email"));
        Assert.That(exception1.Message, Is.EqualTo("email is null or empty! (Parameter 'email')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }



    [Test]
    public async Task TryRegister_Should_Return_TrueOrFalse_Test()
    {

        // add user to db
        User user = new() { Id = 1, Name = "Karolina", Email = "Test@gmail.com", PasswordHash = "hashPass", Role = new() };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();


        var result1 = await _loginService.TryRegister("TrueTest@gmail.com", "123456");
        var result2 = await _loginService.TryRegister("Test@gmail.com", "123456");

        var result3 = await _context.Users.ToListAsync();


        Assert.That(result1, Is.True);
        Assert.That(result2, Is.False);
        Assert.That(result3.Last().Name, Is.EqualTo("TrueTest@gmail.com"));
    }


    [Test]
    public void TryRegister_InputNull_Test()
    {
        var exception1 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryRegister(null!, "password"));
        var exception2 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryRegister("email", null!));

        Assert.That(exception1.ParamName, Is.EqualTo("email"));
        Assert.That(exception1.Message, Is.EqualTo("email is null or empty! (Parameter 'email')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }


    [Test]
    public void TryRegister_InputEmpty_Test()
    {
        var exception1 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryRegister(" ", "password"));
        var exception2 = Assert.ThrowsAsync<ArgumentNullException>(async () => await _loginService.TryRegister("email", " "));

        Assert.That(exception1.ParamName, Is.EqualTo("email"));
        Assert.That(exception1.Message, Is.EqualTo("email is null or empty! (Parameter 'email')"));
        Assert.That(exception2.ParamName, Is.EqualTo("password"));
        Assert.That(exception2.Message, Is.EqualTo("password is null or empty! (Parameter 'password')"));
    }



}
