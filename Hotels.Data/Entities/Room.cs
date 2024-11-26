namespace Hotels.Data.Entities
{
    public abstract class Room
    {
        public int Id { get; set; }
        public abstract int Capacity { get; }
        public abstract string Type { get; }
        public List<Booking> Bookings { get; set; } = [];
    }
}
