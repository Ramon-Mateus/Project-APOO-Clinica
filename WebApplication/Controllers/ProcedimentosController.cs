using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.DAL.Cadastros;
using System.Net;

namespace WebApplication.Controllers
{
    public class ProcedimentosController : Controller
    {
        private ExameDAL exameDAL = new ExameDAL();

        private ActionResult ObterVisaoExamePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Exame exame = exameDAL.ObterExamePorId((long)id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        private ActionResult GravarExame(Exame exame)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    exameDAL.GravarExame(exame);
                    return RedirectToAction("Index");
                }
                return View(exame);
            }
            catch
            {
                return View(exame);
            }
        }


        public ActionResult Index()
        {
            return View(exameDAL.ObterExamesClassificadosPorDescricao());
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exame exame)
        {
            return GravarExame(exame);
        }
        
        public ActionResult Edit(long? id)
        {
            return ObterVisaoExamePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exame exame)
        {
            return GravarExame(exame);
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoExamePorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoExamePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Exame exame = exameDAL.EliminarExamePorId(id);
                TempData["Message"] = "Exame " + exame.Descricao.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private ConsultaDAL consultaDAL = new ConsultaDAL();

        private ActionResult ObterVisaoConsultaPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Consulta consulta = consultaDAL.ObterConsultaPorId((long)id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        private ActionResult GravarConsulta(Consulta consulta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    consultaDAL.GravarConsulta(consulta);
                    return RedirectToAction("Index");
                }
                return View(consulta);
            }
            catch
            {
                return View(consulta);
            }
        }
        public ActionResult IndexConsulta()
        {
            return View(consultaDAL.ObterConsultasClassificadasPorId());
        }

        public ActionResult CreateConsulta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConsulta(Consulta consulta)
        {
            return GravarConsulta(consulta);
        }

        public ActionResult EditConsulta(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConsulta(Consulta consulta)
        {
            return GravarConsulta(consulta);
        }

        public ActionResult DetailsConsulta(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        public ActionResult DeleteConsulta(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConsulta(long id)
        {
            try
            {
                Consulta consulta = consultaDAL.EliminarConsultaPorId(id);
                TempData["Message"] = "Consulta " + consulta.ConsultaId + " foi removida";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
