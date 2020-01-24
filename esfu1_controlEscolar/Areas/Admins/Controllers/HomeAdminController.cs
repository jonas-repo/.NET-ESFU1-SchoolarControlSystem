using esfu1_controlEscolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin, Prefecto, TrabajoSocial, Alumno")]
    public class HomeAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var avisos = db.Avisos.OrderByDescending(x => x.Aviso_fecha).Take(9).ToList();
            ViewBag.avisos = avisos;
            return View();
        }
    }
}