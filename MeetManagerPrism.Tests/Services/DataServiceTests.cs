using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using MeetManagerPrism.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Tests.Services;


[TestFixture]
public class DataServiceTests
{
    private DataService _dataService;
    private AppDbContext _context;

    [SetUp]
    public void Setup()
    {

        var option = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _context = new AppDbContext(option);
        _dataService = new DataService(_context);

    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }



    [Test]
    public async Task AddUser_Should_AddUserToDatabase()
    {

        var user = new User { Email = "user@gmail.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };

        await _dataService.AddUser(user);
        await _dataService.SaveChanges();


        var result = await _dataService.GetUser("user@gmail.com");


        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo("user@gmail.com"));
        Assert.That(result.Role.RoleName, Is.EqualTo("User"));
    }


    [Test]
    public async Task AddEvent_Should_AddEventToDatabase()
    {
        var room = new Room() { Name = "Pub" };
        var eventType = new EventType() { Name = "Party" };


        var role = new Role() { Id = "roleId1", RoleName = "User" };
        var user = new User() { Id = 5, Name = "Karel", Email = "Karel@gmail.com", RoleId = "roleId1", Role = role };
        var newEvent = new Event() { Name = "Party Rock", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Description = "DnB Music!", Autor = user, EventType = eventType, Room = room };

        await _dataService.AddEvent(newEvent);
        await _dataService.SaveChanges();


        var resultEventList = await _dataService.GetEventsList(user);


        Assert.That(resultEventList, Is.Not.Null);
        Assert.That(resultEventList.Count, Is.EqualTo(1));
        Assert.That(resultEventList.First().Name, Is.EqualTo("Party Rock"));
        Assert.That(resultEventList.First().Room.Name, Is.EqualTo("Pub"));
        Assert.That(resultEventList.First().EventType.Name, Is.EqualTo("Party"));
        Assert.That(resultEventList.First().StartDate, Is.LessThan(DateTime.Now.AddSeconds(20)));
        Assert.That(resultEventList.First().StartDate, Is.GreaterThan(DateTime.Now.AddSeconds(-20)));
    }


    [Test]
    public async Task AddEventType_Should_AddEventTypeToDatabase()
    {
        var eventType = new EventType() { Name = "Party", Id = 2 };

        await _dataService.AddEventType(eventType);
        await _dataService.SaveChanges();

        var resultEventType = await _dataService.GetEventTypeList();

        Assert.That(resultEventType, Is.Not.Null);
        Assert.That(resultEventType.Count, Is.EqualTo(1));
        Assert.That(resultEventType.First().Name, Is.EqualTo("Party"));
    }


    [Test]
    public async Task AddRoom_Should_AddEventTypeToDatabase()
    {
        var room = new Room() { Name = "Pub", Capacity = 125, Location = "Paris" };

        await _dataService.AddRoom(room);
        await _dataService.SaveChanges();


        var resultRoomList = await _dataService.GetRoomList();

        Assert.That(resultRoomList, Is.Not.Null);
        Assert.That(resultRoomList.Count, Is.EqualTo(1));
        Assert.That(resultRoomList.First().Name, Is.EqualTo("Pub"));
    }


    [Test]
    public async Task AddInvitation_Should_AddEventTypeToDatabase()
    {
        var newEvent = new Event() { Id = 6, Name = "NewEventTest" };
        var invitation = new Invitation() { Id = 1, Autor = new(), Event = newEvent, SentDate = DateTime.Now, };


        await _dataService.AddInvitation(invitation);
        await _dataService.SaveChanges();


        var result = await _dataService.GetInvitation(newEvent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.SentDate, Is.LessThan(DateTime.Now.AddSeconds(20)));
        Assert.That(result.SentDate, Is.GreaterThan(DateTime.Now.AddSeconds(-20)));
    }


    [Test]
    public async Task UpdateEvent_Should_UpdateEventInDatabase()
    {
        var room = new Room() { ID = 55, Name = "Pub" };
        var eventType = new EventType() { Id = 54, Name = "Party" };
        var user = new User { Id = 4, Email = "user@gmail.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };
        var newEvent = new Event() { Id = 1, AutorId = 4, EventTypeId = 1, InvitationId = 2, RoomID = 3, Name = "Party Rock", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Description = "DnB Music!", Autor = user, EventType = eventType, Room = room };
        var updatedEvent = new Event() { Id = 1, AutorId = 4, EventTypeId = 1, InvitationId = 2, RoomID = 3, Name = "Updated Party Rock", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Description = "DnB Music!", Autor = user, EventType = eventType, Room = room };

        await _dataService.AddEvent(newEvent);
        await _dataService.SaveChanges();

        var resultList1 = await _dataService.GetEventsList(user);
        Assert.That(resultList1.First().Name, Is.EqualTo("Party Rock"));


        _dataService.UpdateEvent(updatedEvent);
        await _dataService.SaveChanges();

        var resultList2 = await _dataService.GetEventsList(user);

        Assert.That(resultList2.First(), Is.Not.Null);
        Assert.That(resultList2.First().Name, Is.EqualTo("Updated Party Rock"));
        Assert.That(resultList2.First().StartDate, Is.LessThan(DateTime.Now.AddMinutes(1)));
        Assert.That(resultList2.First().StartDate, Is.GreaterThan(DateTime.Now.AddMinutes(-1)));
    }



}