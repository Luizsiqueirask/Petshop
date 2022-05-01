using System;

namespace WebAuth.Models.Animal
{
   public class Schedule
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public Places Places { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
