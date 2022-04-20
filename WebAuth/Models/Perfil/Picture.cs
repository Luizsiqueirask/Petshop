using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuth.Models.Perfil
{
    public class Picture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Text)]

        public string Tag { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Path { get; set; }
    }
}
