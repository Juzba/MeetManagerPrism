using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Services;

public interface IDataService
{
    Task AddUser(User user);
    Task AddEvent(Event newEvent);
    Task AddEventType(EventType eventType);
    Task AddRoom(Room room);
    Task AddInvitation(Invitation invitation);


    void UpdateEvent(Event updEvent);
    void UpdateInvitedUser(InvitedUser invitedUser);
    void UpdateInvitation(Invitation invitation);


    Task DeleteEvent(Event delEvent);
    Task DeleteRoom(Room delRoom);
    Task DeleteEventType(EventType delEventType);
    Task DeleteUser(User delUser);

    Task<User?> GetUser(string email);
    Task<InvitedUser?> GetInvitedUser(User user);
    Task<Invitation?> GetInvitation(Event myEvent);

    Task<IEnumerable<User>> GetUsersList();
    Task<IEnumerable<InvitedUser>> GetInvitedUsersList_FromEvent(int eventId);
    Task<IEnumerable<Role>> GetRolesList();
    Task<IEnumerable<Event>> GetEventsList(User user);
    Task<IEnumerable<Event>> GetAceptedEventsList_byInvitedUser(User user);
    Task<IEnumerable<Event>> GetEventsList_byInvitedUser(User user);
    Task<IEnumerable<Event>> GetTodayEventsList(User user);
    Task<IEnumerable<Event>> GetUpcomingEventsList(User user);
    Task<IEnumerable<EventType>> GetEventTypeList();
    Task<IEnumerable<Room>> GetRoomList();

    Task SaveChanges();
}



public class DataService(AppDbContext db) : IDataService
{
    private readonly AppDbContext _db = db;

    // *ADD* //
    // ADD USER //
    public async Task AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        await _db.Users.AddAsync(user);
    }


    // ADD EVENT //
    public async Task AddEvent(Event newEwent)
    {
        if (newEwent == null) throw new ArgumentNullException(nameof(newEwent), "newEwent cannot be null!");

        await _db.Events.AddAsync(newEwent);
    }


    // ADD EVENT-TYPE //
    public async Task AddEventType(EventType eventType)
    {
        if (eventType == null) throw new ArgumentNullException(nameof(eventType), "eventType cannot be null!");

        await _db.EventTypes.AddAsync(eventType);
    }


    // ADD ROOM //
    public async Task AddRoom(Room room)
    {
        if (room == null) throw new ArgumentNullException(nameof(room), "room cannot be null!");

        await _db.Rooms.AddAsync(room);
    }


    // ADD INVITATION //
    public async Task AddInvitation(Invitation invitation)
    {
        if (invitation == null) throw new ArgumentNullException(nameof(invitation), "invitation cannot be null!");

        await _db.Invitations.AddAsync(invitation);
    }



    // *UPDATE* //
    // UPDATE EVENT //
    public void UpdateEvent(Event updEvent)
    {
        if (updEvent == null) throw new ArgumentNullException(nameof(updEvent), "updEvent cannot be null!");

        var local = _db.Events.Local.FirstOrDefault(p => p.Id == updEvent.Id);

        if (local != null) _db.Entry(local).State = EntityState.Detached;
        _db.Events.Update(updEvent);
    }


    // UPDATE INVITED USER //
    public void UpdateInvitedUser(InvitedUser invitedUser)
    {
        if (invitedUser == null) throw new ArgumentNullException(nameof(invitedUser), "invitedUser cannot be null!");

        var local = _db.InvitedUsers.Local.FirstOrDefault(p => p.Id == invitedUser.Id);

        if (local != null) _db.Entry(local).State = EntityState.Detached;
        _db.InvitedUsers.Update(invitedUser);
    }


    // UPDATE INVITATION //
    public void UpdateInvitation(Invitation invitation)
    {
        if (invitation == null) throw new ArgumentNullException(nameof(invitation), "invitation cannot be null!");

        var local = _db.Invitations.Local.FirstOrDefault(p => p.Id == invitation.Id);

        if (local != null)
        {
            _db.Entry(local).State = EntityState.Detached;
        }
        _db.Invitations.Update(invitation);
    }


    // *DELETE* //
    // DELETE EVENT //
    public async Task DeleteEvent(Event delEvent)
    {
        if (delEvent == null) throw new ArgumentNullException(nameof(delEvent), "delEvent cannot be null!");

        var dbEvent = await _db.Events.FindAsync(delEvent.Id);
        if (dbEvent != null) _db.Events.Remove(dbEvent);
    }


    // DELETE ROOM //
    public async Task DeleteRoom(Room delRoom)
    {
        if (delRoom == null) throw new ArgumentNullException(nameof(delRoom), "delRoom cannot be null!");

        var room = await _db.Rooms.FindAsync(delRoom.ID);
        if (room != null) _db.Rooms.Remove(room);
    }


    // DELETE EVENT-TYPE //
    public async Task DeleteEventType(EventType delEventType)
    {
        if (delEventType == null) throw new ArgumentNullException(nameof(delEventType), "delEventType cannot be null!");

        var eventType = await _db.EventTypes.FindAsync(delEventType.Id);
        if (eventType != null) _db.EventTypes.Remove(eventType);
    }


    // DELETE USER //
    public async Task DeleteUser(User delUser)
    {
        if (delUser == null) throw new ArgumentNullException(nameof(delUser), "delUser cannot be null!");

        var user = await _db.Users.FindAsync(delUser.Id);
        if (user != null) _db.Users.Remove(user);
    }


    // *GET* //
    // GET USER //
    public async Task<User?> GetUser(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email), "email cannot be null or empty!");

        return await _db.Users.Include(p => p.Role).FirstOrDefaultAsync(p => p.Email == email);
    }

    // GET INVITED USER //
    public async Task<InvitedUser?> GetInvitedUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.InvitedUsers.FirstOrDefaultAsync(p => p.User.Id == user.Id);
    }


    // GET INVITATION //
    public async Task<Invitation?> GetInvitation(Event myEvent)
    {
        if (myEvent == null) throw new ArgumentNullException(nameof(myEvent), "myEvent cannot be null!");

        return await _db.Invitations.FirstOrDefaultAsync(p => p.Event.Id == myEvent.Id);
    }


    // GET USERS LIST //
    public async Task<IEnumerable<User>> GetUsersList() => await _db.Users.Include(p => p.Role).ToListAsync();


    // GET INVITED-USERS LIST FROM EVENT //
    public async Task<IEnumerable<InvitedUser>> GetInvitedUsersList_FromEvent(int eventId)
    {
        return await _db.InvitedUsers.Where(p => p.Invitation.EventId == eventId).Include(p => p.User).ToListAsync();
    }


    // GET ROLES LIST //
    public async Task<IEnumerable<Role>> GetRolesList() => await _db.Roles.ToListAsync();


    // GET EVENT LIST - USER //
    public async Task<IEnumerable<Event>> GetEventsList(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.Events.Where(p => p.AutorId == user.Id).Include(p => p.EventType).Include(p => p.Room).ToListAsync();
    }

    // GET EVENT LIST ALL - BY INVITED-USER //
    public async Task<IEnumerable<Event>> GetAceptedEventsList_byInvitedUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.Events.Where(
            p => p.Invitation
            .InvitedUsers
            .Any(p => p.User.Id == user.Id && p.Status == InvStatus.Accepted))
            .Include(p => p.Autor)
            .ToListAsync();
    }


    // GET EVENT LIST - INVITED-USER - Pending or rejected //
    public async Task<IEnumerable<Event>> GetEventsList_byInvitedUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.Events.Where(
            p => p.Invitation
            .InvitedUsers
            .Any(p => p.User.Id == user.Id && (p.Status == InvStatus.Pending || p.Status == InvStatus.Rejected)))
            .Include(p => p.Autor)
            .ToListAsync();
    }

    // GET UPCOMING EVENT LIST - USER //
    public async Task<IEnumerable<Event>> GetUpcomingEventsList(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.Events.Where
            (
            p => p.Invitation.InvitedUsers.Any(p => p.UserId == user.Id && p.Status == InvStatus.Accepted)
            && p.StartDate >= DateTime.Now
            )
            .Include(p => p.EventType)
            .Include(p => p.Room)
            .Include(p => p.Autor)
            .Take(10)
            .ToListAsync();
    }


    // GET TODAY EVENT LIST - USER //
    public async Task<IEnumerable<Event>> GetTodayEventsList(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "user cannot be null!");

        return await _db.Events.Where
          (
          p => p.Invitation.InvitedUsers.Any(p => p.UserId == user.Id && p.Status == InvStatus.Accepted)
          && p.StartDate <= DateTime.Now
          && p.EndDate >= DateTime.Now
          )
          .Include(p => p.EventType).Include(p => p.Room).Include(p => p.Autor).ToListAsync();
    }


    // GET ROOMS LIST //
    public async Task<IEnumerable<Room>> GetRoomList() => await _db.Rooms.ToListAsync();


    // GET EVENT TYPE LIST //
    public async Task<IEnumerable<EventType>> GetEventTypeList() => await _db.EventTypes.ToListAsync();


    // *SAVE* //
    // SAVE USERS LIST //
    public async Task SaveChanges() => await _db.SaveChangesAsync();
}
