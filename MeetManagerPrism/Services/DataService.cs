using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Services
{
    public interface IDataService
    {
        Task AddUser(User user);
        Task AddEvent(Event newEvent);
        Task AddEventType(EventType eventType);
        Task AddRoom(Room room);
        Task AddInvitation(Invitation invitation);

        Task UpdateEvent(Event updEvent);
        Task UpdateInvitation(Invitation invitation);

        Task DeleteEvent(Event delEvent);
        Task DeleteRoom(Room room);
        Task DeleteEventType(EventType eventType);
        Task DeleteUser(User user);

        Task<User?> GetUser(string email);
        Task<InvitedUser?> GetInvitedUser(User user);
        Task<Invitation?> GetInvitation(Event myEvent);

        Task<ICollection<User>> GetUsersList();
        Task<ICollection<InvitedUser>> GetInvitedUsersList_FromEvent(int eventId);
        Task<ICollection<Role>> GetRolesList();
        Task<ICollection<Event>> GetEventsList(User user);
        Task<ICollection<Event>> GetEventsList_byInvitedUser(User user);
        Task<ICollection<Event>> GetTodayEventsList(User user);
        Task<ICollection<Event>> GetUpcomingEventsList(User user);
        Task<ICollection<EventType>> GetEventTypeList();
        Task<ICollection<Room>> GetRoomList();

        Task SaveChangesDB();
    }



    public class DataService(AppDbContext db) : IDataService
    {
        private readonly AppDbContext _db = db;


        // ADD USER //
        public async Task AddUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        // ADD EVENT //
        public async Task AddEvent(Event newEwent)
        {
            await _db.Events.AddAsync(newEwent);
            await _db.SaveChangesAsync();
        }

        // ADD EVENT-TYPE //
        public async Task AddEventType(EventType eventType)
        {
            await _db.EventTypes.AddAsync(eventType);
            await _db.SaveChangesAsync();
        }

        // ADD ROOM //
        public async Task AddRoom(Room room)
        {
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
        }

        // ADD INVITATION //
        public async Task AddInvitation(Invitation invitation)
        {
            await _db.Invitations.AddAsync(invitation);
            await _db.SaveChangesAsync();
        }

        // UPDATE EVENT //
        public async Task UpdateEvent(Event updEwent)
        {
            _db.Events.Attach(updEwent);
            _db.Entry(updEwent).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        // UPDATE INVITATION //
        public async Task UpdateInvitation(Invitation invitation)
        {
            _db.Invitations.Attach(invitation);
            _db.Entry(invitation).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        // DELETE EVENT //
        public async Task DeleteEvent(Event delEvent)
        {
            var dbEvent = await _db.Events.FindAsync(delEvent.Id);
            if (dbEvent == null) return;

            _db.Events.Remove(dbEvent);
            await _db.SaveChangesAsync();
        }

        // DELETE ROOM //
        public async Task DeleteRoom(Room room)
        {
            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();
        }

        // DELETE EVENT-TYPE //
        public async Task DeleteEventType(EventType eventType)
        {
            _db.EventTypes.Remove(eventType);
            await _db.SaveChangesAsync();
        }

        // DELETE USER //
        public async Task DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }


        // GET USER //
        public async Task<User?> GetUser(string email) => await _db.Users.Include(p => p.Role).FirstOrDefaultAsync(p => p.Email == email);
      
        // GET INVITED USER //
        public async Task<InvitedUser?> GetInvitedUser(User user) => await _db.InvitedUsers.FirstOrDefaultAsync(p=>p.User == user);

        // GET INVITATION //
        public async Task<Invitation?> GetInvitation(Event myEvent) => await _db.Invitations.FirstOrDefaultAsync(p => p.Event == myEvent);

        // GET USERS LIST //
        public async Task<ICollection<User>> GetUsersList() => await _db.Users.Include(p => p.Role).ToListAsync();

        // GET INVITED-USERS LIST FROM EVENT //
        public async Task<ICollection<InvitedUser>> GetInvitedUsersList_FromEvent(int eventId) => await _db.InvitedUsers.Where(p => p.Invitation.EventId == eventId).Include(p => p.User).ToListAsync();


        // GET ROLES LIST //
        public async Task<ICollection<Role>> GetRolesList() => await _db.Roles.ToListAsync();


        // GET EVENT LIST - USER //
        public async Task<ICollection<Event>> GetEventsList(User user) => await _db.Events.Where(p => p.AutorId == user.Id).Include(p => p.EventType).Include(p => p.Room).ToListAsync();

        // GET EVENT LIST - INVITED-USER //
        public async Task<ICollection<Event>> GetEventsList_byInvitedUser(User user) => await _db.Events.Where(p => p.Invitation.InvitedUsers.Any(p=>p.User == user)).Include(p=>p.Autor).ToListAsync();

        // GET TODAY EVENT LIST - USER //
        public async Task<ICollection<Event>> GetUpcomingEventsList(User user)
        {
            return await _db.Events.Where
                (
                p => p.AutorId == user.Id
                && p.StartDate >= DateTime.Now
                )
                .Include(p => p.EventType)
                .Include(p => p.Room)
                .Include(p => p.Autor)
                .Take(10)
                .ToListAsync();
        }

        // GET UPCOMING EVENT LIST - USER //
        public async Task<ICollection<Event>> GetTodayEventsList(User user)
        {
            return await _db.Events.Where
                (
                p => p.AutorId == user.Id
                && p.StartDate <= DateTime.Now
                && p.EndDate >= DateTime.Now
                )
                .Include(p => p.EventType).Include(p => p.Room).Include(p => p.Autor).ToListAsync();
        }

        // GET ROOMS LIST //
        public async Task<ICollection<Room>> GetRoomList() => await _db.Rooms.ToListAsync();

        // GET EVENT TYPE LIST //
        public async Task<ICollection<EventType>> GetEventTypeList() => await _db.EventTypes.ToListAsync();

        // SAVE USERS LIST //
        public async Task SaveChangesDB() => await _db.SaveChangesAsync();


    }
}
