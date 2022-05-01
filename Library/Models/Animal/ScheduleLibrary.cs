using System;

namespace Library.Models.Animal
{
    public class ScheduleLibrary
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
