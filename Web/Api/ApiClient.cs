using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Web.Models.Animal;
using Web.Models.Perfil;

namespace Web.Api
{
    public class ApiClient
    {
        public readonly HttpClient _clientAnimal;
        public readonly HttpClient _clientPerfil;
        private readonly List<int> ports = new List<int>() { 62678, 60341 };

        public ApiClient()
        {

            _clientAnimal = new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:{ports[0]}/")
            };

            _clientPerfil = new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:{ports[1]}/")
            };

            _clientAnimal.DefaultRequestHeaders.Accept.Clear();
            _clientPerfil.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");

            _clientAnimal.DefaultRequestHeaders.Accept.Add(mediaType);
            _clientPerfil.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        #region Person
        public async Task<HttpResponseMessage> GetPerson()
        {
            return await _clientPerfil.GetAsync("api/Person");
        }
        public async Task<HttpResponseMessage> GetPersonById(int? Id)
        {
            return await _clientPerfil.GetAsync($"api/Person/{Id}");
        }
        public async Task<HttpResponseMessage> PostPerson(Person person)
        {
            return await _clientPerfil.PostAsJsonAsync("api/Person", person);
        }
        public async Task<HttpResponseMessage> PutPerson(Person person, int? Id)
        {
            return await _clientPerfil.PutAsJsonAsync($"api/Person/{Id}", person);
        }
        public async Task<HttpResponseMessage> DeletPerson(int? Id)
        {
            return await _clientPerfil.DeleteAsync($"api/Person/{Id}");
        }
        #endregion Person

        #region Pet
        public async Task<HttpResponseMessage> GetFriends()
        {
            return await _clientAnimal.GetAsync("api/Pet");
        }
        public async Task<HttpResponseMessage> GetFriendsById(int? Id)
        {
            return await _clientAnimal.GetAsync($"api/Pet/{Id}");
        }
        public async Task<HttpResponseMessage> PostFriends(Pet pet)
        {
            return await _clientAnimal.PostAsJsonAsync("api/Pet", pet);
        }
        public async Task<HttpResponseMessage> PutFriends(Pet pet, int? Id)
        {
            return await _clientAnimal.PutAsJsonAsync($"api/Pet/{Id}", pet);
        }
        public async Task<HttpResponseMessage> DeleteFriends(int? Id)
        {
            return await _clientAnimal.DeleteAsync($"api/Pet/{Id}");
        }
        #endregion Pet
    }
}