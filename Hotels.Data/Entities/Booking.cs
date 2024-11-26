namespace Hotels.Data.Entities
{
    public class Booking(Hotel hotel, Room room, DateTime startDate, DateTime endDate)
    {
        private Booking() : this(null!, null!, DateTime.MinValue, DateTime.MinValue) { } // need for EF
        public int Id { get; private set; }
        public Hotel Hotel { get; private set; } = hotel;
        public Room Room { get; private set; } = room;
        public DateTime StartDate { get; private set; } = startDate;
        public DateTime EndDate { get; private set; } = endDate;
        public string Reference { get; private set; } = Guid.NewGuid().ToString();
    }
}
