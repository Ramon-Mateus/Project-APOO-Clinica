using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Context;
using WebApplication.DAL.Cadastros;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers.Procedimentos
{
    public class ConsultasController : Controller
    {

        private ConsultaDAL consultaDAL = new ConsultaDAL();
        private EFContext context = new EFContext();

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
        public ActionResult Index()
        {
            return View(consultaDAL.ObterConsultasClassificadasPorId());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consulta consulta)
        {
            return GravarConsulta(consulta);
        }

        public ActionResult Edit(long? id)
        {
            // return ObterVisaoConsultaPorId(id);
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
            var ConsultasExames = from c in context.Exames
                                  select new
                                  {
                                      c.ExameId,
                                      c.Descricao,
                                      Checked = ((from ce in context.ConsultaExames
                                                  where (ce.ConsultaId == id) & (ce.ExameId == c.ExameId)
                                                  select ce).Count() > 0)
                                  };
            var consultasViewModel = new ConsultasViewModel();
            consultasViewModel.ConsultaId = id.Value;
            consultasViewModel.Data_hora = consulta.DataHora;
            consultasViewModel.Sintomas = consulta.Sintomas;
            var checkboxListExames = new List<CheckBoxViewModel>();
            foreach (var item in ConsultasExames)
            {
                checkboxListExames.Add(new CheckBoxViewModel
                {
                    Id = item.ExameId,
                    Descricao = item.Descricao,
                    Checked = item.Checked
                });
            }
            consultasViewModel.Exames = checkboxListExames;
            return View(consultasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConsultasViewModel consulta)
        {
            // return GravarConsulta(consulta);
            if (ModelState.IsValid)
            {
                var consultaSelecionada = context.Consultas.Find(consulta.ConsultaId);
                consultaSelecionada.ConsultaId = consulta.ConsultaId;
                consultaSelecionada.DataHora = consulta.Data_hora;
                consultaSelecionada.Sintomas = consulta.Sintomas;
                foreach (var item in context.ConsultaExames)
                {
                    if (item.ConsultaId == consulta.ConsultaId)
                    {
                        context.Entry(item).State = EntityState.Deleted;
                    }
                }
                if (consulta.Exames != null)
                {
                    foreach (var item in consulta.Exames)
                    {
                        if (item.Checked)
                        {
                            context.ConsultaExames.Add(new ConsultaExame()
                            {
                                ConsultaId = consulta.ConsultaId,
                                ExameId = item.Id
                            });
                        }
                    }
                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consulta);
        }

        public ActionResult Details(long? id)
        {
            // return ObterVisaoConsultaPorId(id);
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
            var ConsultasExames = from c in context.Exames
                                  select new
                                  {
                                      c.ExameId,
                                      c.Descricao,
                                      Checked = ((from ce in context.ConsultaExames
                                                  where (ce.ConsultaId == id) & (ce.ExameId == c.ExameId)
                                                  select ce).Count() > 0)
                                  };
            var consultaViewModel = new ConsultasViewModel();
            consultaViewModel.ConsultaId = id.Value;
            consultaViewModel.Data_hora = consulta.DataHora;
            consultaViewModel.Sintomas = consulta.Sintomas;
            var checkboxListExames = new List<CheckBoxViewModel>();
            foreach (var item in ConsultasExames)
            {
                checkboxListExames.Add(new CheckBoxViewModel
                {
                    Id = item.ExameId,
                    Descricao = item.Descricao,
                    Checked = item.Checked
                });
            }
            consultaViewModel.Exames = checkboxListExames;
            return View(consultaViewModel);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
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