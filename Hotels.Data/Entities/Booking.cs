namespace Hotels.Data.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reference { get; set; } = Guid.NewGuid().ToString();
    }
}
