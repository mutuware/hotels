﻿namespace Hotels.Data.Entities
{
    public class SingleRoom : Room
    {
        public override int Capacity => 1;

        public override string Type => "Single";
    }
}
