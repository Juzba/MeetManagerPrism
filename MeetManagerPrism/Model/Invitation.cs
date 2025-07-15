namespace MeetManagerWPF.Model
{
    public class Invitation
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public InvStatus Status { get; set; } = InvStatus.Pending;

        // Event
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;

        // User
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        // User Participants
        public ICollection<User> Participants { get; set; } = [];
    }


    public enum InvStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
