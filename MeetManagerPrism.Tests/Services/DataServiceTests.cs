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
    public async Task AddEvent_Should_AddEventToDatabase()
    {
        var room = new Room() { Name = "Pub", Capacity = 125, Location = "Paris" };
        var eventType = new EventType() { Name = "Party" };


        var role = new Role() { Id = "roleId1", RoleName = "User" };
        var user = new User() { Id = 5, Name = "Karel", Email = "Karel@gmail.com", RoleId = "roleId1", Role = role };
        var newEvent = new Event() { Name = "Party Rock", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Description = "DnB Music!", Autor = user, EventType = eventType, Room = room };

        await _dataService.AddEvent(newEvent);
        await _dataService.SaveChanges();


        var resultEventList = await _dataService.GetEventsList(user);
        

        Assert.That(resultEventList, Is.Not.Null);
        Assert.That(resultEventList.Count, Is.GreaterThan(0));
        Assert.That(resultEventList.First().Name , Is.EqualTo("Party Rock"));
        Assert.That(resultEventList.First().Room.Name , Is.EqualTo("Pub"));
        Assert.That(resultEventList.First().EventType.Name , Is.EqualTo("Party"));
    }

  






}