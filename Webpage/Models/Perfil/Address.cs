using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webpage.Models.Perfil
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string States { get; set; }
        public string City { get; set; }
        public string Neighborhoods { get; set; }
    }
}
