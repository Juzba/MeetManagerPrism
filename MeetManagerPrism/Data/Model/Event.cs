namespace MeetManagerPrism.Data.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        // EVENT TYPE //
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; } = null!;


        // ROOM //
        public int RoomID { get; set; }
        public Room Room { get; set; } = null!;


        // USER //
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
