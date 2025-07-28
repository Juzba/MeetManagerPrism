namespace MeetManagerPrism.Data.Model
{
    public class Invitation
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public InvStatus Status { get; set; } = InvStatus.Pending;

        // Event
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;

        // Autor - User
        public int AutorId { get; set; }
        public User Autor { get; set; } = default!;

        // InvitedUsers
        public ICollection<InvitedUser> InvitedUsers { get; set; } = [];
    }


    public enum InvStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
