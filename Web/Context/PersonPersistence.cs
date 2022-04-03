using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Api;
using Web.Models.Perfil;

namespace Web.Context
{
    public class PersonPersistence : Controller
    {
        private readonly ApiClient _clientPerson;
        private readonly BlobClient _blobClient;
        //private readonly HttpPostedFileBase httpPosted;

        public PersonPersistence()
        {
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }

        public async Task<IEnumerable<Person>> List()
        {
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
                    return listPerson;
                }
            }

            return new List<Person>();
        }
        public async Task<Person> Get(int? Id)
        {

            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();

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
                return _person;
            }

            return new Person();
        }
        public async Task<Person> Create()
        {
            return new Person();
        }
        public async Task<Boolean> Post(Person person, HttpPostedFileBase httpPosted)
        {
            try
            {
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    await _blobClient.SetupCloudBlob();

                    var getBlobName = _blobClient.GetRandomBlobName(httpPosted.FileName);
                    var blobContainer = _blobClient._blobContainer.GetBlockBlobReference(getBlobName);
                    await blobContainer.UploadFromStreamAsync(httpPosted.InputStream);

                    person.Picture.Tag = blobContainer.Name.ToString();
                    person.Picture.Path = blobContainer.Uri.AbsolutePath.ToString();

                    await _clientPerson.PostPerson(person);
                    return true;
                }
                return false;
            }
            catch
            {
                var directoryPath = @"~/Images/Person/";
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    var PictureName = Path.GetFileName(httpPosted.FileName);
                    var PictureExt = Path.GetExtension(PictureName);
                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        var PicturePath = Path.Combine(Server.MapPath(directoryPath), PictureName);

                        person.Picture.Tag = PictureName;
                        person.Picture.Path = PicturePath;

                        httpPosted.SaveAs(person.Picture.Path);
                        await _clientPerson.PostPerson(person);
                    }
                    return true;
                }
                return false;
            }
        }
        public async Task<Person> Update(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return person;
            }
            return new Person();
        }
        public async Task<Boolean> Put(Person person, int? Id, HttpPostedFileBase httpPosted)
        {
            try
            {
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    await _blobClient.SetupCloudBlob();

                    var getBlobName = _blobClient.GetRandomBlobName(httpPosted.FileName);
                    var blobContainer = _blobClient._blobContainer.GetBlockBlobReference(getBlobName);
                    await blobContainer.UploadFromStreamAsync(httpPosted.InputStream);

                    person.Picture.Tag = blobContainer.Name.ToString();
                    person.Picture.Path = blobContainer.Uri.AbsolutePath.ToString();

                    await _clientPerson.PostPerson(person);
                    return true;
                }
                return false;
            }
            catch
            {
                var directoryPath = @"~/Images/Flags/Countries/";
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    var PictureName = Path.GetFileName(httpPosted.FileName);
                    var PictureExt = Path.GetExtension(PictureName);
                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        var PicturePath = Path.Combine(Server.MapPath(directoryPath), PictureName);

                        person.Picture.Tag = PictureName;
                        person.Picture.Path = PicturePath;

                        httpPosted.SaveAs(person.Picture.Path);
                        await _clientPerson.PostPerson(person);
                    }
                    return true;
                }
                return false;
            }
        }
        public async Task<Person> Delete(int? Id)
        {
            try
            {
                var people = await _clientPerson.GetPersonById(Id);

                if (people.IsSuccessStatusCode)
                {
                    var person = await people.Content.ReadAsAsync<Person>();
                    return person;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }

            return new Person();
        }
        public async Task<Person> Delete(int? Id, Person person)
        {
            try
            {
                var people = await _clientPerson.DeletePerson(Id);

                if (people.IsSuccessStatusCode)
                {
                    await people.Content.ReadAsAsync<Person>();
                    return person;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }
            return new Person();
        }
    }
}