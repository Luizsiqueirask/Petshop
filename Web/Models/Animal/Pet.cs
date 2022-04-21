using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.Perfil;

namespace Web.Models.Animal
{
    public class Pet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe nome")]
        [DisplayName("Nome do seu animal")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe tipo de animal de estimação.")]
        [DisplayName("tipo animal de estimação")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Informe a idade seu animal")]
        [DisplayName("Idade do seu animal")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Inserido Genero")]
        [DisplayName("Genero do seu animal")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Informe aniversário")]
        [DisplayName("Data de aniversário do seu animal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Birthday { get; set; }
        public Image Image { get; set; }
        public Health Health { get; set; }
        public Schedule Schedule { get; set; }

        [Required(ErrorMessage = "Informe o nome do dono do animal")]
        [DisplayName("Dono do animal")]
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
