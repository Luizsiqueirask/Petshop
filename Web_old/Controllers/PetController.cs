using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Context;
using Web.Models.Animal;

namespace Web.Controllers
{
    public class PetController : Controller
    {
        private readonly PetPersistence clientPet;

        public PetController()
        {
            clientPet = new PetPersistence();
        }

        // GET: Pet
        public async Task<ActionResult> Index()
        {
            var pet = await clientPet.List();
            return View(pet);
        }

        // GET: Pet/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            var pet = await clientPet.Get(Id);
            return View(pet);
        }

        // GET: Pet/Create
        public async Task<ActionResult> Create()
        {
            var pet = await clientPet.Create();
            return View(pet);
        }

        // POST: Pet/Create
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet, HttpPostedFileBase httpPosted)
        {
            try
            {
                // TODO: Add insert logic here
                await clientPet.Post(pet, httpPosted);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Pet());
            }
        }

        // GET: Pet/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {
            var pet = await clientPet.Update(Id);
            return View(pet);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Pet pet, int? Id, HttpPostedFileBase httpPosted)
        {
            try
            {
                // TODO: Add update logic here
                await clientPet.Put(pet, Id, httpPosted);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Pet());
            }
        }

        // GET: Pet/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            var pet = await clientPet.Get(Id);
            return View(pet);
        }

        // POST: Pet/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                // TODO: Add delete logic here
                await clientPet.Delete(Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Pet());
            }
        }
    }
}
