namespace MeetManagerPrism.Data.Model
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Events
        public ICollection<Event> Events { get; set; } = [];

    }
}
