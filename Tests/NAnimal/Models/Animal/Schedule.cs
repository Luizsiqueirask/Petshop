using System;

namespace Library.Models.Animal
{
    public class Places
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public Places Places { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}