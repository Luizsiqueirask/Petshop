using Perfil.Casting;
using Perfil.Models.Perfil;
using System.Collections.Generic;
using System.Web.Http;

namespace Perfil.Controllers
{
    public class PersonController : ApiController
    {
        private readonly PersonCast personCast;

        public PersonController()
        {
            personCast = new PersonCast();
        }

        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            return personCast.List();
        }

        // GET: api/Person/5
        public Person Get(int? Id)
        {
            return personCast.Get(Id);
        }

        // POST: api/Person
        public void Post(Person person)
        {
            personCast.Post(person);
        }

        // PUT: api/Person/5
        public void Put(Person person, int? Id)
        {
            personCast.Put(person, Id);
        }

        // DELETE: api/Person/5
        public void Delete(int? Id)
        {
            personCast.Delete(Id);
        }
    }
}
