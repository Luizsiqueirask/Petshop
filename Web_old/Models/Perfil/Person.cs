﻿using System;

namespace Web.Models.Perfil
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Enum Genre { get; set; }
        public DateTime Birthday { get; set; }
        public Picture Picture { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }
}