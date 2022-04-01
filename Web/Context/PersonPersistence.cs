using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Models.Perfil;
using Web.Api;

namespace Web.Context
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


        public async Task<IEnumerable<Person>> List() {
            var allPeople = await _clientPerson.GetPerson();
            var listPerson = new List<Person>();
            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();

                foreach (var person in people)
                {
                    var _person = new Person()
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Age = person.Age,
                        Birthday = person.Birthday,
                        Genre = person.Genre,
                        Picture = new Picture()
                        {
                            Id = person.Picture.Id,
                            Tag = person.Picture.Tag,
                            Path = person.Picture.Path
                        },
                        Contact = new Contact()
                        {
                            Id = person.Contact.Id,
                            Email = person.Contact.Email,
                            Mobile = person.Contact.Mobile
                        },
                        Address = new Address()
                        {
                            Id = person.Address.Id,
                            Country = person.Address.Country,
                            States = person.Address.States,
                            City = person.Address.City,
                            Neighborhoods = person.Address.Neighborhoods
                        }
                    };
                    listPerson.Add(_person);
                }
                return listPerson;
            }

            return new List<Person>();
        }
        /*public async Task<Person> Get(int? Id) { return new Person(); }
        public async Task<Person> Create(int? Id) { return new Person(); }
        public async Task<Boolean> Post(Person person) { return false; }
        public async Task<Person> Update(int? Id) { return new Person(); }
        public async Task<Boolean> Put(Person person, int? Id) { return false; }
        public async Task<Person> Delete(int? Id) { return new Person(); }
        public async Task<Person> Delete(int? Id, Person person) { return new Person(); }*/
    }
}