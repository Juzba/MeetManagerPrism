namespace MeetManagerPrism.Data.Model
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;

        // Events
        public ICollection<Event> Events { get; set; } = [];
    }
}
