using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Animal
{
    public class PetLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Age { get; set; }
        public DateTime Birthday { get; set; }
        public ImageLibrary Image { get; set; }
        public HealthLibrary Health { get; set; }
        public ServiceLibrary Service { get; set; }
        public int PersonId { get; set; }
    }
}
