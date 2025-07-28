namespace MeetManagerPrism.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;


        // Role
        public string RoleId { get; set; } = "UserRoleId-54sa9-sda87";
        public Role Role { get; set; } = default!;


        // EVENTS //
        public ICollection<Event> MyEvents { get; set; } = [];


        // INVITATION //
        public ICollection<Invitation> MyInvitations { get; set; } = [];


        // INVITED - USERS PARTICIPANTS //
        public ICollection<InvitedUser> InvitedUsers { get; set; } = [];

    }
}
