using Library.Context.Perfil;
using Library.Models.Perfil;
using Perfil.Models.Perfil;
using System.Collections.Generic;

namespace Perfil.Casting
{
    public class PersonCast
    {
        private readonly ClassPerson classPerson;
        public PersonCast()
        {
            classPerson = new ClassPerson();
        }

        public IEnumerable<Person> List()
        {
            var listPersonApi = new List<Person>();
            var allPerson = classPerson.List();

            if (allPerson != null)
            {
                foreach (var people in allPerson)
                {
                    var person = new Person()
                    {
                        Id = people.Id,
                        FirstName = people.FirstName,
                        LastName = people.LastName,
                        Age = people.Age,
                        Birthday = people.Birthday,
                        Genre = people.Genre,
                        Picture = new Picture()
                        {
                            Id = people.Picture.Id,
                            Tag = people.Picture.Tag,
                            Path = people.Picture.Path
                        },
                        Contact = new Contact()
                        {
                            Id = people.Contact.Id,
                            Email = people.Contact.Email,
                            Mobile = people.Contact.Mobile
                        },
                        Address = new Address()
                        {
                            Id = people.Address.Id,
                            Country = people.Address.Country,
                            States = people.Address.States,
                            City = people.Address.City,
                            Neighborhoods = people.Address.Neighborhoods
                        }
                    };

                    listPersonApi.Add(person);
                }

                return listPersonApi;
            }
            else
            {
                return null;
            }
        }
        public Person Get(int? Id)
        {
            var people = classPerson.Get(Id);

            if (people != null)
            {
                var person = new Person()
                {
                    Id = people.Id,
                    FirstName = people.FirstName,
                    LastName = people.LastName,
                    Age = people.Age,
                    Birthday = people.Birthday,
                    Genre = people.Genre,
                    Picture = new Picture()
                    {
                        Id = people.Picture.Id,
                        Tag = people.Picture.Tag,
                        Path = people.Picture.Path
                    },
                    Contact = new Contact()
                    {
                        Id = people.Contact.Id,
                        Email = people.Contact.Email,
                        Mobile = people.Contact.Mobile
                    },
                    Address = new Address()
                    {
                        Id = people.Address.Id,
                        Country = people.Address.Country,
                        States = people.Address.States,
                        City = people.Address.City,
                        Neighborhoods = people.Address.Neighborhoods
                    }
                };
                return person;
            }
            else
            {
                return null;
            }
        }
        public bool Post(Person person)
        {
            if (person != null)
            {
                var personLibrary = new PersonLibrary()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday = person.Birthday,
                    Age = person.Age,
                    Genre = person.Genre,
                    Picture = new PictureLibrary()
                    {
                        Id = person.Picture.Id,
                        Tag = person.Picture.Tag,
                        Path = person.Picture.Path
                    },
                    Contact = new ContactLibrary()
                    {
                        Id = person.Contact.Id,
                        Email = person.Contact.Email,
                        Mobile = person.Contact.Mobile
                    },
                    Address = new AddressLibrary()
                    {
                        Id = person.Address.Id,
                        Country = person.Address.Country,
                        States = person.Address.States,
                        City = person.Address.City,
                        Neighborhoods = person.Address.Neighborhoods
                    }
                };
                classPerson.Post(personLibrary);
                return true;
            }
            return false;
        }
        public bool Put(Person person, int? Id)
        {
            if (person != null)
            {
                var personLibrary = new PersonLibrary()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday = person.Birthday,
                    Age = person.Age,
                    Genre = person.Genre,

                    Picture = new PictureLibrary()
                    {
                        Id = person.Picture.Id,
                        Tag = person.Picture.Tag,
                        Path = person.Picture.Path
                    },
                    Contact = new ContactLibrary()
                    {
                        Id = person.Contact.Id,
                        Email = person.Contact.Email,
                        Mobile = person.Contact.Mobile
                    },
                    Address = new AddressLibrary()
                    {
                        Id = person.Address.Id,
                        Country = person.Address.Country,
                        States = person.Address.States,
                        City = person.Address.City,
                        Neighborhoods = person.Address.Neighborhoods
                    }
                };
                classPerson.Put(personLibrary, Id);
                return true;
            }
            return false;
        }
        public void Delete(int? Id)
        {
            classPerson.Delete(Id);
        }
    }
}