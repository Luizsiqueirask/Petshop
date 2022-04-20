using Library.Models.Perfil;
using System.Collections.Generic;

namespace Library.Context.PerfilAuth
{
    public interface INterfaceAuthPerson
    {
        IEnumerable<PersonLibrary> List();
        PersonLibrary Get(int? Id);
        void Post(PersonLibrary personLibrary);
        void Put(PersonLibrary personLibrary, int? Id);
        void Delete(int? Id);
    }
}
