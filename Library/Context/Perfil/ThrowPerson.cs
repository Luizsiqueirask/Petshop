using Library.Models.Perfil;
using System;
using System.Collections.Generic;

namespace Library.Context.Perfil
{
    public class ThrowPerson : INterfacePerson
    {
        public IEnumerable<PersonLibrary> List()
        {
            throw new NotImplementedException();
        }
        public PersonLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(PersonLibrary personLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(PersonLibrary personLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
