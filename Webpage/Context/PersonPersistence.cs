using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Webpage.Api;
using Webpage.Models.Perfil;

namespace Webpage.Context
{
    public class PersonPersistence
    {
        private readonly ApiClient _clientPerson;
        private readonly BlobClient _blobClient;

        public PersonPersistence()
        {
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }
    }

    /*public async Task<IEnumerable<Person>> List() { return new List<Person>(); }
    public async Task<Person> Get(int? Id) { return new Person(); }
    public async Task<Person> Create(int? Id) { return new Person(); }
    public async Task<Boolean> Post(Person person) { return false; }
    public async Task<Person> Update(int? Id) { return new Person(); }
    public async Task<Boolean> Put(Person person, int? Id) { return false; }
    public async Task<Person> Delete(int? Id) { return new Person(); }
    public async Task<Person> Delete(int? Id, Person person) { return new Person(); }*/

}