using System;

namespace Library.Models.Animal
{
    public class PlacesLibrary
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }

    public class ScheduleLibrary
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public PlacesLibrary Places { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
