using MeetManagerPrism.Data;
using MeetManagerPrism.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetManagerPrism.Services
{
    public interface IDataService
    {
        Task AddUser(User user);
        Task AddEvent(Event newEvent);
        Task DeleteUser(User user);
        Task<User?> GetUser(string email);
        Task<ICollection<User>> GetUsersList();
        Task<ICollection<Role>> GetRolesList();
        Task<ICollection<EventType>> GetEventTypeList();
        Task<ICollection<Room>> GetRoomList();
        Task UpdateUsersList();
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

        // DELETE USER //
        public async Task DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }


        // GET USER //
        public async Task<User?> GetUser(string email) => await _db.Users.Include(p => p.Role).FirstOrDefaultAsync(p => p.Email == email);

        // GET USERS LIST //
        public async Task<ICollection<User>> GetUsersList() => await _db.Users.Include(p => p.Role).ToListAsync();

         // GET USERS LIST //
        public async Task<ICollection<Role>> GetRolesList() => await _db.Roles.ToListAsync();

        // GET EVENT TYPE LIST //
        public async Task<ICollection<EventType>> GetEventTypeList() => await _db.EventTypes.ToListAsync();

        // GET EVENT TYPE LIST //
        public async Task<ICollection<Room>> GetRoomList() => await _db.Rooms.ToListAsync();

        // SAVE USERS LIST //
        public async Task UpdateUsersList() => await _db.SaveChangesAsync();


    }
}
