namespace Hotels.Data.Entities
{
    public class DeluxeRoom : Room
    {
        public override int Capacity => 2;

        public override string Type => "Deluxe";
    }
}
