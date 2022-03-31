using Library.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Context.Animal
{
    public class ThrowPet : INterfacePet
    {
        public IEnumerable<PetLibrary> List()
        {
            throw new NotImplementedException();
        }
        public PetLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(PetLibrary petLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(PetLibrary petLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
