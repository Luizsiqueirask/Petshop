using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Perfil
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe e-mail")]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe nunero mobile")]
        [DisplayName("Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
    }
}
