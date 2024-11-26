namespace Hotels.Api.Models
{
    public class CreateBookingDTO
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
