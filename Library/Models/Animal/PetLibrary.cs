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
        public Enum Genre { get; set; }
        public ImagesLibrary Images { get; set; }
        public HealthLibrary Health { get; set; }
        public ServiceLibrary Service { get; set; }
        public int PersonId { get; set; }
    }
}
