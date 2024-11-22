namespace Hotels.Data.Entities
{
    public class DoubleRoom : Room
    {
        public override int Capacity => 2;

        public override string Type => "Double";
    }
}
