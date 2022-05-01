using System;

namespace Animal.Models.Animal
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public Image Image { get; set; }
        public Health Health { get; set; }
        public Schedule Schedule { get; set; }
        public int PersonId { get; set; }
    }
}
