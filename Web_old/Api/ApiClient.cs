﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Models.Animal;
using Web.Models.Perfil;

namespace Web.Api
{
    public class ApiClient
    {
        public readonly HttpClient _clientAnimal;
        public readonly HttpClient _clientPerfil;
        private readonly List<int> ports = new List<int>() { 44377, 44379 };

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
        public async Task<HttpResponseMessage> DeletePerson(int? Id)
        {
            return await _clientPerfil.DeleteAsync($"api/Person/{Id}");
        }
        #endregion Person

        #region Pet
        public async Task<HttpResponseMessage> GetPet()
        {
            return await _clientAnimal.GetAsync("api/Pet");
        }
        public async Task<HttpResponseMessage> GetPetById(int? Id)
        {
            return await _clientAnimal.GetAsync($"api/Pet/{Id}");
        }
        public async Task<HttpResponseMessage> PostPet(Pet pet)
        {
            return await _clientAnimal.PostAsJsonAsync("api/Pet", pet);
        }
        public async Task<HttpResponseMessage> PutPet(Pet pet, int? Id)
        {
            return await _clientAnimal.PutAsJsonAsync($"api/Pet/{Id}", pet);
        }
        public async Task<HttpResponseMessage> DeletePet(int? Id)
        {
            return await _clientAnimal.DeleteAsync($"api/Pet/{Id}");
        }
        #endregion Pet
    }
}