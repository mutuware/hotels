using Microsoft.EntityFrameworkCore;

namespace Hotels.Data.Entities
{
    public class Hotel(string name, List<Room> rooms)
    {
        private Hotel() : this(string.Empty, []) { }

        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public List<Room> Rooms { get; private set; } = rooms;
    }
}
