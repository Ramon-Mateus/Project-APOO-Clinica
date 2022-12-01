using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.DAL.Cadastros;
using WebApplication.Models;

namespace WebApplication.Controllers.Animal
{
    public class PetsController : Controller
    {
        private PetDAL petDAL = new PetDAL();

        private ActionResult ObterVisaoPetPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Pet pet = petDAL.ObterPetPorId((long)id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        private ActionResult GravarPet(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    petDAL.GravarPet(pet);
                    return RedirectToAction("Index");
                }
                return View(pet);
            }
            catch
            {
                return View(pet);
            }
        }
        public ActionResult Index()
        {
            return View(petDAL.ObterPetsClassificadosPorId());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pet pet)
        {
            return GravarPet(pet);
        }

        public ActionResult Edit(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            return GravarPet(pet);
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Pet pet = petDAL.EliminarPetPorId(id);
                TempData["Message"] = "Pet " + pet.PetId + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}