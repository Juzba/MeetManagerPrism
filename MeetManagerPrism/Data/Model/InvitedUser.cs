namespace MeetManagerPrism.Data.Model;


public class InvitedUser
{
    public int Id { get; set; }


    // Invitation
    public int InvitationId { get; set; }
    public Invitation Invitation { get; set; } = default!;


    // User
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
