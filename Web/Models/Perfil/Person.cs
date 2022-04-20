using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Perfil
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe nome")]
        [DisplayName("Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Informe sobrenome")]
        [DisplayName("Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Informe idade")]
        [DisplayName("Idade")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Inserido Genero")]
        [DisplayName("Genero")]
        public string Genre { get; set; }
     
        [Required(ErrorMessage = "Informe nome")]
        [DisplayName("Data de aniversário")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, HtmlEncode = true)]
        public DateTime Birthday { get; set; }
        public Picture Picture { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }
}
