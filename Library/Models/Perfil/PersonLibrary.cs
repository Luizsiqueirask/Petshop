using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Perfil
{
    public class PersonLibrary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Enum Genre { get; set; }
        public DateTime Birthday { get; set; }
        public PictureLibrary Picture { get; set; }
        public ContactLibrary Contact { get; set; }
        public AddressLibrary Address { get; set; }
    }
}
