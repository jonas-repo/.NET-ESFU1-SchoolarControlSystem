using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using esfu1_controlEscolar.Areas.Admins.Models;
using esfu1_controlEscolar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin, Prefecto")]
    public class ReporteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ReporteController()
        {
        }

        public ReporteController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admins/Reporte
        public ActionResult Index()
        {
            return View(db.Alumno.ToList());
        }



        // GET: Admins/Reporte/Create
        public ActionResult Create(Guid? id)
        {
            
            var alumno = db.Alumno.Find(id);
            if(alumno == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.alumno = alumno;
            return View();
        }

        // POST: Admins/Reporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reporte reporte)
        {
                reporte.Reporte_id = Guid.NewGuid();
                reporte.Alumno = db.Alumno.Find(reporte.Alumno.Alumno_id);
                db.Reporte.Add(reporte);
                db.SaveChanges();

            var user = db.Users.Where(x => x.UserName.Equals(reporte.Alumno.Usuario_id)).FirstOrDefault();
            if(user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if(emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "REPORTE ESFU 1 : " + reporte.Reporte_fecha, reporte.Reporte_descripcion);
            }            
            return RedirectToAction("Index");
        }

        // GET: Admins/Reporte/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reporte = db.Reporte.Where(x => x.Reporte_id == id).Include("Alumno").FirstOrDefault();
            

            if (reporte == null)
            {
                return HttpNotFound();
            }
            return View(reporte);
        }

        // POST: Admins/Reporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reporte reporte)
        {
                reporte.Alumno = db.Alumno.Find(reporte.Alumno.Alumno_id);
                db.Entry(reporte).State = EntityState.Modified;
                db.SaveChanges();
            var user = db.Users.Where(x => x.UserName.Equals(reporte.Alumno.Usuario_id)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "REPORTE ESFU 1 : " + reporte.Reporte_fecha, reporte.Reporte_descripcion);
            }
            return RedirectToAction("Index");


        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporte reporte = db.Reporte.Find(id);
            if (reporte == null)
            {
                return HttpNotFound();
            }
            db.Reporte.Remove(reporte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
