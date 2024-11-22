namespace Hotels.Data.Entities
{
    public abstract class Room
    {
        public int Id { get; set; }
        public abstract int Capacity { get; }
    }
}
