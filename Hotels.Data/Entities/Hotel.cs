namespace Hotels.Data.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}
