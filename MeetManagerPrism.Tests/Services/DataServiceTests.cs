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
    public async Task AddUser_Should_AddUser_ToDatabase()
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
    public async Task AddEvent_Should_AddEvent_ToDatabase()
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
    public async Task AddEventType_Should_AddEventType_ToDatabase()
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
    public async Task AddRoom_Should_AddRoom_ToDatabase()
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
    public async Task AddInvitation_Should_AddInvitation_ToDatabase()
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
    public async Task UpdateEvent_Should_UpdateEvent_InDatabase()
    {
        var user = new User { Id = 4, Email = "user@gmail.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };
        var newEvent = new Event() { Id = 1, AutorId = 4, EventTypeId = 1, InvitationId = 2, RoomID = 3, Name = "Party Rock", StartDate = new(), EndDate = new(), Description = "DnB Music!", Autor = user, EventType = new(), Room = new() };
        var updatedEvent = new Event() { Id = 1, AutorId = 4, EventTypeId = 1, InvitationId = 2, RoomID = 3, Name = "Updated Party Rock", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Description = "DnB Music!", Autor = user, EventType = new(), Room = new() };

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



    [Test]
    public async Task UpdateInvitedUser_Should_UpdateInvitedUser_InDatabase()
    {
        var user = new User { Id = 4, Email = "user@gmail.com", Role = new() { Id = "id-1", RoleName = "User" }, RoleId = "id-1" };
        var invitedUser = new InvitedUser() { Id = 1, User = user, Invitation = new(), Status = InvStatus.Pending };
        var updatedInvitedUser = new InvitedUser() { Id = 1, User = user, Invitation = new(), Status = InvStatus.Accepted };

        await _context.InvitedUsers.AddAsync(invitedUser);
        await _dataService.SaveChanges();

        var dataInDb = await _context.InvitedUsers.FirstAsync();
        Assert.That(dataInDb.Status, Is.EqualTo(InvStatus.Pending));

        _dataService.UpdateInvitedUser(updatedInvitedUser);
        await _dataService.SaveChanges();

        var result = await _dataService.GetInvitedUser(user);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(InvStatus.Accepted));
    }


    [Test]
    public async Task UpdateInvitation_Should_UpdateInvitation_InDatabase()
    {
        var testEvent = new Event() { Id = 1, Name = "NewEventTest" };
        var invitation = new Invitation() { EventId = 1, Id = 1, AutorId = 1, Event = testEvent, SentDate = new() };
        var updatedInvitation = new Invitation() { EventId = 1, Id = 1, AutorId = 2, Event = testEvent, SentDate = DateTime.Now };


        await _context.Invitations.AddAsync(invitation);
        await _dataService.SaveChanges();

        var dataInDb = await _context.Invitations.FirstAsync();
        Assert.That(dataInDb.AutorId, Is.EqualTo(1));
        Assert.That(dataInDb.Id, Is.EqualTo(1));

        _dataService.UpdateInvitation(updatedInvitation);
        await _dataService.SaveChanges();

        var result = await _dataService.GetInvitation(testEvent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.AutorId, Is.EqualTo(2));
        Assert.That(result.SentDate, Is.LessThan(DateTime.Now.AddMinutes(1)));
        Assert.That(result.SentDate, Is.GreaterThan(DateTime.Now.AddMinutes(-1)));
    }



    [Test]
    public async Task DeleteEvent_Should_DeleteEvent_InDatabase()
    {
        var user = new User() { Id = 1, Name = "Josef" };
        Event testEvent1 = new() { Id = 1, Name = "NewEventTest1", AutorId = 1, Autor = user, EventType = new(), Invitation = new(), Room = new() };
        Event testEvent2 = new() { Id = 2, Name = "NewEventTest2", AutorId = 1, Autor = user, EventType = new(), Invitation = new(), Room = new() };
        Event testEvent3 = new() { Id = 3, Name = "NewEventTest3", AutorId = 1, Autor = user, EventType = new(), Invitation = new(), Room = new() };

        List<Event> events = [testEvent1, testEvent2, testEvent3];

        await _context.Events.AddRangeAsync(events);


        await _dataService.DeleteEvent(testEvent2);
        await _dataService.SaveChanges();

        var result = await _dataService.GetEventsList(user);
        var result2 = await _context.Events.FindAsync(2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result2, Is.Null);
    }


    [Test]
    public async Task DeleteRoom_Should_DeleteRoom_InDatabase()
    {
        Room test1 = new() { ID = 1, Name = "NewTest1" };
        Room test2 = new() { ID = 2, Name = "NewTest2" };
        Room test3 = new() { ID = 3, Name = "NewTest3" };

        List<Room> tests = [test1, test2, test3];

        await _context.Rooms.AddRangeAsync(tests);


        await _dataService.DeleteRoom(test2);
        await _dataService.SaveChanges();

        var result = await _dataService.GetRoomList();
        var result2 = await _context.Rooms.FindAsync(2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result2, Is.Null);
    }


    [Test]
    public async Task DeleteEventType_Should_DeleteEventType_InDatabase()
    {
        EventType test1 = new() { Id = 1, Name = "NewTest1" };
        EventType test2 = new() { Id = 2, Name = "NewTest2" };
        EventType test3 = new() { Id = 3, Name = "NewTest3" };

        List<EventType> tests = [test1, test2, test3];

        await _context.EventTypes.AddRangeAsync(tests);


        await _dataService.DeleteEventType(test2);
        await _dataService.SaveChanges();

        var result = await _dataService.GetEventTypeList();
        var result2 = await _context.EventTypes.FindAsync(2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result2, Is.Null);
    }


    [Test]
    public async Task DeleteUser_Should_DeleteUser_InDatabase()
    {
        User test1 = new() { Id = 1, Name = "NewTest1", Role = new Role() { Id = "1", RoleName = "testRole1" } };
        User test2 = new() { Id = 2, Name = "NewTest2", Role = new Role() { Id = "2", RoleName = "testRole2" } };
        User test3 = new() { Id = 3, Name = "NewTest3", Role = new Role() { Id = "3", RoleName = "testRole3" } };

        List<User> tests = [test1, test2, test3];

        await _context.Users.AddRangeAsync(tests);


        await _dataService.DeleteUser(test2);
        await _dataService.SaveChanges();

        var result = await _dataService.GetUsersList();
        var result2 = await _context.Users.FindAsync(2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.First().Role, Is.Not.Null);
        Assert.That(result.First().Role.RoleName, Is.EqualTo("testRole1"));
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result2, Is.Null);
    }


    [Test]
    public async Task GetUsersList_Should_GetUsersList_FromDatabase()
    {
        User test1 = new() { Id = 1, Name = "NewTest1", Role = new Role() { Id = "1", RoleName = "testRole1" } };
        User test2 = new() { Id = 2, Name = "NewTest2", Role = new Role() { Id = "2", RoleName = "testRole2" } };
        User test3 = new() { Id = 3, Name = "NewTest3", Role = new Role() { Id = "3", RoleName = "testRole3" } };

        List<User> tests = [test1, test2, test3];

        await _context.Users.AddRangeAsync(tests);
        await _dataService.SaveChanges();


        var result = await _dataService.GetUsersList();


        Assert.That(result, Is.Not.Null);
        Assert.That(result.First().Role, Is.Not.Null);
        Assert.That(result.First().Role.RoleName, Is.EqualTo("testRole1"));
        Assert.That(result.Count, Is.EqualTo(3));
    }


    //[Test]
    //public async Task GetInvitedUsersList_FromEvent_Should_GetInvitedUsers_FromDatabase()
    //{
    //    InvitedUser test1 = new() { Id = 1,    };
    //    InvitedUser test2 = new() { Id = 2, };
    //    InvitedUser test3 = new() { Id = 3,  };

    //    List<User> tests = [test1, test2, test3];

    //    await _context.Users.AddRangeAsync(tests);
    //    await _dataService.SaveChanges();


    //    var result = await _dataService.GetUsersList();


    //    Assert.That(result, Is.Not.Null);
    //    Assert.That(result.First().Role, Is.Not.Null);
    //    Assert.That(result.First().Role.RoleName, Is.EqualTo("testRole1"));
    //    Assert.That(result.Count, Is.EqualTo(3));
    //}



}