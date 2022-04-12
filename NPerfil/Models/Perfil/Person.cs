using System;

namespace Perfil.Models.Perfil
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
        public DateTime Birthday { get; set; }
        public Picture Picture { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }
}
