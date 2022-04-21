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
    public class PersonPersistenceOld : Controller
    {
        private readonly ApiClient _clientPerson;
        private readonly BlobClient _blobClient;
        //private readonly HttpPostedFileBase httpPosted;

        public PersonPersistenceOld()
        {
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }

        public async Task<IEnumerable<Person>> List()
        {
            var allPeople = await _clientPerson.GetPerson();

            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();
                return people;
            }

            return new List<Person>();
        }
        public async Task<Person> Get(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return person;
            }

            return new Person();
        }
        public async Task<Person> Create()
        {
            return new Person();
        }
        public async Task<Person> Post(Person person, HttpPostedFileBase postedFileBase)
        {
            // https://www.c-sharpcorner.com/article/upload-and-display-image-in-asp-net-core-3-1/
            // https://docs.microsoft.com/pt-br/dotnet/api/system.web.ui.webcontrols.fileupload.postedfile?view=netframework-4.8
            // https://cpratt.co/file-uploads-in-asp-net-mvc-with-view-models/

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var pictureNameBlob = _blobClient.GetRandomBlobName(postedFileBase.FileName);
                    var picturePathblob = _blobClient._blobContainer.GetBlockBlobReference(pictureNameBlob);
                    await picturePathblob.UploadFromStreamAsync(postedFileBase.InputStream);

                    person.Picture.Tag = picturePathblob.Name.ToString();
                    person.Picture.Path = picturePathblob.Uri.AbsolutePath.ToString();

                    await _clientPerson.PostPerson(person);
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    var directoryPath = @"../Uploads/Person/";

                    // Create pictute on server
                    var pictureName = Path.GetFileName(postedFileBase.FileName);
                    var rootPath = Server.MapPath(directoryPath);
                    var picturePath = Path.Combine(rootPath, pictureName);

                    // Add picture reference to model and save
                    // var pictureLocalPath = string.Concat(directoryPath, pictureName);
                    var PictureExt = Path.GetExtension(pictureName);

                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        person.Picture.Tag = pictureName;
                        person.Picture.Path = picturePath;

                        postedFileBase.SaveAs(picturePath);
                        await _clientPerson.PostPerson(person);
                    }
                }
            }
            return new Person();
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