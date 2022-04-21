﻿using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Context;
using Web.Models.Perfil;

namespace Web.Controllers
{
    public class PersonControllerOld : Controller
    {
        private readonly PersonPersistenceOld clientPerson;

        public PersonControllerOld()
        {
            clientPerson = new PersonPersistenceOld();
        }

        // GET: Person
        public async Task<ActionResult> Index()
        {
            var person = await clientPerson.List();
            return View(person);
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var person = await clientPerson.Get(Id);
            return View(person);
        }

        // GET: Person/Create
        public async Task<ActionResult> Create()
        {
            var person = await clientPerson.Create();
            return View(person);
        }

        // POST: Person/Create
        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            HttpFileCollectionBase httpFileCollection = Request.Files;
            HttpPostedFileBase postedFileBase = httpFileCollection[0];

            try
            {
                // TODO: Add insert logic here               
                await clientPerson.Post(person, postedFileBase);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Person());
            }
        }

        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var person = await clientPerson.Update(Id);
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Person person, int? Id, HttpPostedFileBase httpPosted)
        {
            try
            {
                // TODO: Add update logic here
                await clientPerson.Put(person, Id, httpPosted);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Person());
            }
        }

        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            var person = await clientPerson.Get(Id);
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                // TODO: Add delete logic here
                await clientPerson.Delete(Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Person());
            }
        }
    }
}