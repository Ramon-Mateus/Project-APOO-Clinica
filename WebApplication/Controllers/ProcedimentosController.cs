using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProcedimentosController : Controller
    {

        private static IList<Exame> exames = new List<Exame>()
        {
            new Exame() { ExameId = 0, Descricao = "Próstata"},
            new Exame() { ExameId = 1, Descricao = "Vista"},
            new Exame() { ExameId = 2, Descricao = "Sangue"},
            new Exame() { ExameId = 3, Descricao = "Tomografia"},
            new Exame() { ExameId = 4, Descricao = "Ressonância"},
        };

        // GET: Exames
        public ActionResult Index()
        {
            return View(exames);
        }

        // GET: Exames/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exames/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Exames/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Exames/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Exames/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Exames/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
