﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Api;
using Web.Context;
using Web.Models.Perfil;

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonPersistence clientPerson;
        private readonly ApiClient _clientPerson;
        private readonly BlobClient _blobClient;

        public PersonController()
        {
            clientPerson = new PersonPersistence();
            _clientPerson = new ApiClient();
            _blobClient = new BlobClient();
        }

        // GET: Person
        public async Task<ActionResult> Index()
        {
            /*
             var path = Server.MapPath("~/images/"); 
             string[] imagesFiles = Directory.GetFiles(Path)
             ViewBag.images = imagesFiles;
            */
            var allPeople = await _clientPerson.GetPerson();

            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();
                return View(people);
            }

            return View(new List<Person>());
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return View(person);
            }

            return View(new Person());
        }

        // GET: Person/Create
        public async Task<ActionResult> Create()
        {
            return View(new Person());
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            // https://www.c-sharpcorner.com/article/upload-and-display-image-in-asp-net-core-3-1/
            // https://docs.microsoft.com/pt-br/dotnet/api/system.web.ui.webcontrols.fileupload.postedfile?view=netframework-4.8
            // https://cpratt.co/file-uploads-in-asp-net-mvc-with-view-models/

            HttpFileCollectionBase httpFileCollection = Request.Files;
            FileUpload fileUpload = new FileUpload();

            try
            {
                if (ModelState.IsValid)
                {
                    await _blobClient.SetupCloudBlob();

                    var pictureNameBlob = _blobClient.GetRandomBlobName(httpFileCollection[0].FileName);
                    var picturePathblob = _blobClient._blobContainer.GetBlockBlobReference(pictureNameBlob);
                    await picturePathblob.UploadFromStreamAsync(httpFileCollection[0].InputStream);

                    person.Picture.Tag = picturePathblob.Name.ToString();
                    person.Picture.Path = picturePathblob.Uri.AbsolutePath.ToString();

                    await _clientPerson.PostPerson(person);
                }
            }
            catch
            {
                if (ModelState.IsValid)
                {
                    var directoryPath = @"../Web/Uploads/Person/";

                    // Create pictute on server
                    var pictureName = Path.GetFileName(httpFileCollection[0].FileName);
                    var picturePath = Server.MapPath(Path.Combine(directoryPath, pictureName));

                    //Add picture reference to model and save
                    var pictureLocalPath = string.Concat(directoryPath, pictureName);
                    var PictureExt = Path.GetExtension(pictureName);

                    if (PictureExt.Equals(".jpg") || PictureExt.Equals(".jpeg") || PictureExt.Equals(".png"))
                    {
                        person.Picture.Tag = pictureName;
                        person.Picture.Path = pictureLocalPath;
                        fileUpload.SaveAs(picturePath);

                        Debug.WriteLine(person.Picture.Path);
                        await _clientPerson.PostPerson(person);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(new Person());
        }

        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var people = await _clientPerson.GetPersonById(Id);

            if (people.IsSuccessStatusCode)
            {
                var person = await people.Content.ReadAsAsync<Person>();
                return View(person);
            }
            return View(new Person());
        }

        // POST: Person/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Person person, int? Id, HttpPostedFileBase httpPosted)
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
                }
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
                }
            }
            return View();
        }

        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            try
            {
                var people = await _clientPerson.GetPersonById(Id);

                if (people.IsSuccessStatusCode)
                {
                    var person = await people.Content.ReadAsAsync<Person>();
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }

            return View(new Person());
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var person = await _clientPerson.DeletePerson(Id);

                if (person.IsSuccessStatusCode)
                {
                    await person.Content.ReadAsAsync<Person>();
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }
            return View(new Person());
        }
    }
}
