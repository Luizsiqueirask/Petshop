using System;

namespace Perfil.Models.Perfil
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int PersonId { get; set; }
    }
}