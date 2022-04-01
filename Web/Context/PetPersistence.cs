using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Webpage.Api;
using Webpage.Models.Animal;
using Webpage.Models.Perfil;

namespace Web.Context
{
    public class PetPersistence
    {
        private readonly ApiClient _clientPerson;
        private readonly BlobClient _blobClient;

        public PetPersistence()
        {
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }
    }

    /*public async Task<IEnumerable<Pet>> List() { return new List<Pet>(); }
    public async Task<Pet> Get(int? Id) { return new Pet(); }
    public async Task<Pet> Create(int? Id) { return new Pet(); }
    public async Task<Boolean> Post(Pet pet) { return false; }
    public async Task<Pet> Update(int? Id) { return new Pet(); }
    public async Task<Boolean> Put(Pet person, int? Id) { return false; }
    public async Task<Pet> Delete(int? Id) { return new Pet(); }
    public async Task<Pet> Delete(int? Id, Pet pet) { return new Pet(); }*/
}