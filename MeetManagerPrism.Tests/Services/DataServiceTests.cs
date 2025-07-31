using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Tests.Services;



public class DataServiceTests
{
    private DataService _dataService;

    [SetUp]
    public void Setup()
    {

        var option = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var context = new AppDbContext(option);
        _dataService = new DataService(context);

    }

    [Test]
    public async Task AddUser_Should_AddUserToDatabase()
    {
     
        var user = new User { Email = "test@example.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };

        await _dataService.AddUser(user);
        await _dataService.SaveChanges();


        var result = await _dataService.GetUser("test@example.com"); 


        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo("test@example.com"));
        Assert.That(result.Role.RoleName, Is.EqualTo("User"));
    }

    [Test]
    public async Task AddUser_Null()
    {

        //var user = new User { Email = "test@example.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };

        await _dataService.AddUser(null!);
        await _dataService.SaveChanges();


        var result = await _dataService.GetUser("test@example.com");


        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo("test@example.com"));
        Assert.That(result.Role.RoleName, Is.EqualTo("User"));
    }







}