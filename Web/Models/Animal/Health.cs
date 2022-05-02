using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Animal
{
    public class Health
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserido estado de saúde do seu animal")]
        [DisplayName("Estado de saúde")]
        public string Status { get; set; }
    }
}
