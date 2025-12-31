using System.ComponentModel.DataAnnotations;

namespace GolfBagManagerAPI
{
    public abstract class Club
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
        public string Brand { get; set; }
        public int Distance { get; set; }

        protected Club() { } // Required by Entity Framework

        protected Club(string type, string brand, int distance)
        {
            Type = type;
            Brand = brand;
            Distance = distance;
        }
    }

    public class Driver : Club
    {
        public Driver() { }
        public Driver(string brand, int distance)
            : base("Driver", brand, distance) { }
    }

    public class Wood : Club
    {
        public int Number { get; set; }

        public Wood() { }
        public Wood(string brand, int distance, int number)
            : base($"{number}-Wood", brand, distance)
        {
            Number = number;
        }
    }

    public class Iron : Club
    {
        public int Number { get; set; }

        public Iron() { }
        public Iron(string brand, int distance, int number)
            : base($"{number}-Iron", brand, distance)
        {
            Number = number;
        }
    }

    public class Wedge : Club
    {
        public string WedgeType { get; set; }

        public Wedge() { }
        public Wedge(string brand, int distance, string wedgeType)
            : base($"{wedgeType} Wedge", brand, distance)
        {
            WedgeType = wedgeType;
        }
    }

    public class Putter : Club
    {
        public Putter() { }
        public Putter(string brand, int distance)
            : base("Putter", brand, distance) { }
    }

    public class GenericClub : Club
    {
        public GenericClub() { }
        public GenericClub(string type, string brand, int distance)
            : base(type, brand, distance) { }
    }
}
