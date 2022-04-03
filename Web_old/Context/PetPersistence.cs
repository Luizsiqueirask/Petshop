﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Api;
using Web.Models.Animal;
using Web.Models.Perfil;

namespace Web.Context
{
    public class PetPersistence : Controller
    {
        private readonly ApiClient _clientPet;
        private readonly BlobClient _blobClient;
        //private readonly HttpPostedFileBase httpPosted;

        public PetPersistence()
        {
            _clientPet = new ApiClient();
            _blobClient = new BlobClient();
        }

        public async Task<IEnumerable<PersonPet>> List()
        {
            var allPets = await _clientPet.GetPet();
            var allPeople = await _clientPet.GetPerson();
            var containerPersonPet = new List<PersonPet>();

            if (allPets.IsSuccessStatusCode)
            {
                var pets = await allPets.Content.ReadAsAsync<IEnumerable<Pet>>();

                if (allPeople.IsSuccessStatusCode)
                {
                    var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();

                    foreach (var pet in pets)
                    {
                        foreach (var person in people)
                        {
                            var personPet = new PersonPet()
                            {
                                Person = person,
                                Pet = pet,
                                PeoplePetsSelect = new List<SelectListItem>() {
                                    new SelectListItem()
                                    {
                                        Value = person.Id.ToString(),
                                        Text = person.FirstName,
                                        Selected = pet.PersonId == person.Id
                                    }
                                }
                            };
                            containerPersonPet.Add(personPet);
                        }
                    }
                    return containerPersonPet;
                }
            }
            return new List<PersonPet>();
        }
        public async Task<PersonPet> Get(int? Id)
        {

            var allPets = await _clientPet.GetPetById(Id);

            if (allPets.IsSuccessStatusCode)
            {
                var pet = await allPets.Content.ReadAsAsync<Pet>();
                var allPeople = await _clientPet.GetPerson();

                if (allPeople.IsSuccessStatusCode)
                {
                    var person = await allPeople.Content.ReadAsAsync<Person>();

                    var personPet = new PersonPet()
                    {
                        Person = person,
                        Pet = pet,
                        PersonPetSelect = new SelectListItem()
                        {
                            Value = person.Id.ToString(),
                            Text = person.FirstName,
                            Selected = pet.PersonId == person.Id
                        }
                    };
                    return personPet;
                }
            }
            return new PersonPet();
        }
        public async Task<Pet> Create()
        {
            return new Pet();
        }
        public async Task<Boolean> Post(Pet pet, HttpPostedFileBase httpPosted)
        {
            try
            {
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    await _blobClient.SetupCloudBlob();

                    var getBlobName = _blobClient.GetRandomBlobName(httpPosted.FileName);
                    var blobContainer = _blobClient._blobContainer.GetBlockBlobReference(getBlobName);
                    await blobContainer.UploadFromStreamAsync(httpPosted.InputStream);

                    pet.Image.Tag = blobContainer.Name.ToString();
                    pet.Image.Path = blobContainer.Uri.AbsolutePath.ToString();

                    await _clientPet.PostPet(pet);
                    return true;
                }
                return false;
            }
            catch
            {
                var directoryPath = @"~/Images/Pet/";
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    var ImageName = Path.GetFileName(httpPosted.FileName);
                    var ImageExt = Path.GetExtension(ImageName);
                    if (ImageExt.Equals(".jpg") || ImageExt.Equals(".jpeg") || ImageExt.Equals(".png"))
                    {
                        var ImagePath = Path.Combine(Server.MapPath(directoryPath), ImageName);

                        pet.Image.Tag = ImageName;
                        pet.Image.Path = ImagePath;

                        httpPosted.SaveAs(pet.Image.Path);
                        await _clientPet.PostPet(pet);
                    }
                    return true;
                }
                return false;
            }
        }
        public async Task<Pet> Update(int? Id)
        {
            var pets = await _clientPet.GetPetById(Id);

            if (pets.IsSuccessStatusCode)
            {
                var pet = await pets.Content.ReadAsAsync<Pet>();
                return pet;
            }
            return new Pet();
        }
        public async Task<Boolean> Put(Pet pet, int? Id, HttpPostedFileBase httpPosted)
        {
            try
            {
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    await _blobClient.SetupCloudBlob();

                    var getBlobName = _blobClient.GetRandomBlobName(httpPosted.FileName);
                    var blobContainer = _blobClient._blobContainer.GetBlockBlobReference(getBlobName);
                    await blobContainer.UploadFromStreamAsync(httpPosted.InputStream);

                    pet.Image.Tag = blobContainer.Name.ToString();
                    pet.Image.Path = blobContainer.Uri.AbsolutePath.ToString();

                    await _clientPet.PostPet(pet);
                    return true;
                }
                return false;
            }
            catch
            {
                var directoryPath = @"~/Images/Pet/";
                if (httpPosted != null && httpPosted.ContentLength > 0)
                {
                    var ImageName = Path.GetFileName(httpPosted.FileName);
                    var ImageExt = Path.GetExtension(ImageName);
                    if (ImageExt.Equals(".jpg") || ImageExt.Equals(".jpeg") || ImageExt.Equals(".png"))
                    {
                        var ImagePath = Path.Combine(Server.MapPath(directoryPath), ImageName);

                        pet.Image.Tag = ImageName;
                        pet.Image.Path = ImagePath;

                        httpPosted.SaveAs(pet.Image.Path);
                        await _clientPet.PutPet(pet, Id);
                    }
                    return true;
                }
                return false;
            }
        }
        public async Task<Pet> Delete(int? Id)
        {
            try
            {
                var pets = await _clientPet.GetPetById(Id);

                if (pets.IsSuccessStatusCode)
                {
                    var pet = await pets.Content.ReadAsAsync<Pet>();
                    return pet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }

            return new Pet();
        }
        public async Task<Pet> Delete(int? Id, Pet pet)
        {
            try
            {
                var pets = await _clientPet.DeletePet(Id);

                if (pets.IsSuccessStatusCode)
                {
                    await pets.Content.ReadAsAsync<Pet>();
                    return pet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG: {ex.Message}");
            }
            return new Pet();
        }
    }
}