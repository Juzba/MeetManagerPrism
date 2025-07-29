namespace MeetManagerPrism.Data.Model;


public class InvitedUser
{
    public int Id { get; set; }
    public InvStatus Status { get; set; } = InvStatus.Pending;


    // Invitation
    public int InvitationId { get; set; }
    public Invitation Invitation { get; set; } = default!;


    // User
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}

public enum InvStatus
{
    Pending,
    Accepted,
    Rejected
}
