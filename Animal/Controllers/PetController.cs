using Animal.Casting;
using Library.Models.Animal;
using System.Collections.Generic;
using System.Web.Http;

namespace Animal.Controllers
{
    public class PetController : ApiController
    {
        private readonly PetCast petCast;

        public PetController()
        {
            petCast = new PetCast();
        }

        // GET: api/Pet
        public IEnumerable<Pet> Get()
        {
            return petCast.List();
        }

        // GET: api/Pet/5
        public Pet Get(int? Id)
        {
            return petCast.Get(Id);
        }

        // POST: api/Pet
        public void Post(Pet pet)
        {
            petCast.Post(pet);
        }

        // PUT: api/Pet/5
        public void Put(Pet pet, int? Id)
        {
            petCast.Put(pet, Id);
        }

        // DELETE: api/Pet/5
        public void Delete(int? Id)
        {
            petCast.Delete(Id);
        }
    }
}
