namespace MeetManagerPrism.Model
{
    public class Role
    {
        public string Id { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        // Users
        public ICollection<User> Users { get; set; } = [];
    }
}
