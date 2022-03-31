using Library.Models.Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Context.Perfil
{
    public interface INterfacePerson
    {
        IEnumerable<PersonLibrary> List();
        PersonLibrary Get(int? Id);
        void Post(PersonLibrary personLibrary);
        void Put(PersonLibrary personLibrary, int? Id);
        void Delete(int? Id);
    }
}
