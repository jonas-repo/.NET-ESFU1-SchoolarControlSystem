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
    [Authorize(Roles = "Admin, TrabajoSocial")]
    public class CitatorioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CitatorioController()
        {
        }

        public CitatorioController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Admins/Citatorio
        public ActionResult Index()
        {
            return View(db.Alumno.ToList());
        }

        

        // GET: Admins/Citatorio/Create
        public ActionResult Create(Guid? id)
        {
            var alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.alumno = alumno;
            return View();
        }

        // POST: Admins/Citatorio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Citatorio citatorio)
        {

                citatorio.Citatorio_id = Guid.NewGuid();
                citatorio.Alumno = db.Alumno.Find(citatorio.Alumno.Alumno_id);
                db.Citatorio.Add(citatorio);
                db.SaveChanges();
            var user = db.Users.Where(x => x.UserName.Equals(citatorio.Alumno.Usuario_id)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "CITATORIO ESFU 1 : " + citatorio.Citatorio_fecha, citatorio.Citatorio_descripcion);
            }
            return RedirectToAction("Index");

        }

        // GET: Admins/Citatorio/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var citatorio = db.Citatorio.Where(x => x.Citatorio_id == id).Include("Alumno").FirstOrDefault();
            if (citatorio == null)
            {
                return HttpNotFound();
            }
            return View(citatorio);
        }

        // POST: Admins/Citatorio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Citatorio citatorio)
        {
            citatorio.Alumno = db.Alumno.Find(citatorio.Alumno.Alumno_id);
                db.Entry(citatorio).State = EntityState.Modified;
                db.SaveChanges();
            var user = db.Users.Where(x => x.UserName.Equals(citatorio.Alumno.Usuario_id)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "CITATORIO ESFU 1 : " + citatorio.Citatorio_fecha, citatorio.Citatorio_descripcion);
            }
            return RedirectToAction("Index");


        }


        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citatorio citatorio = db.Citatorio.Find(id);
            if (citatorio == null)
            {
                return HttpNotFound();
            }
            db.Citatorio.Remove(citatorio);
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
