using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Animal
{
    public class Health
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserido estado de saude do seu animal")]
        [DisplayName("Estado de saude do seu animal")]
        public string Status { get; set; }
    }
}
