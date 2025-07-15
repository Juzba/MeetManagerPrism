using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetManagerWPF.Model
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
    }
}
