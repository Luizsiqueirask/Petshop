using Library.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Context.Animal
{
    public interface INterfacePet
    {
        IEnumerable<PetLibrary> List();
        PetLibrary Get(int? Id);
        void Post(PetLibrary petLibrary);
        void Put(PetLibrary petLibrary, int? Id);
        void Delete(int? Id);
    }
}
