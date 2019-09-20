using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;

namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (CrudEntities db = new CrudEntities() ) { //esta es la conexion a la base de datos
                lst = (from d in db.tablas
                           select new ListTablaViewModel
                           {
                               Id = d.id,
                               Nombre = d.nombre,
                               Correo = d.correo
                           }).ToList();
            }
            return View(lst);
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = new tabla();
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.tablas.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");
                }

                return View(model);

                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }


        public ActionResult Editar(int Id)
        {
            TablaViewModel model = new TablaViewModel();
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.tablas.Find(Id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento = oTabla.fecha_nacimiento.Value;
                model.Id = oTabla.id;
            }

                return View(model);
        }
        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = db.tablas.Find(model.Id);
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");
                }

                return View(model);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.tablas.Find(Id);
                db.tablas.Remove(oTabla);
                db.SaveChanges();
               
            }

            return Redirect("~/Tabla/");
        }

    }
}