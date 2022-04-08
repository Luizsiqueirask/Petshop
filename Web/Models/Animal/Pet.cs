using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.Perfil;

namespace Web.Models.Animal
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Age { get; set; }
        public DateTime Birthday { get; set; }
        public Image Image { get; set; }
        public Health Health { get; set; }
        public Service Service { get; set; }
        public int PersonId { get; set; }
        public IEnumerable<SelectListItem> PersonPetsSelect { get; set; }
    }
    public class PeoplePets
    {
        public Person People { get; set; }
        public Pet Pets { get; set; }
        public IEnumerable<SelectListItem> PeoplePetsSelect { get; set; }
    }
    public class PersonPet
    {
        public Person Person { get; set; }
        public Pet Pet { get; set; }
        public SelectListItem PersonPetSelect { get; set; }
    }
}
