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
    [Authorize(Roles = "Admin")]
    public class DocumentosFaltantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public DocumentosFaltantesController()
        {
        }

        public DocumentosFaltantesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Admins/DocumentosFaltantes
        public ActionResult Index()
        {
            return View(db.Alumno.ToList());
        }

       

        // GET: Admins/DocumentosFaltantes/Create
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

        // POST: Admins/DocumentosFaltantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentosFaltantes documentosFaltantes)
        {
                documentosFaltantes.DocumentosFaltantes_id = Guid.NewGuid();
                documentosFaltantes.Alumno = db.Alumno.Find(documentosFaltantes.Alumno.Alumno_id);
                db.DocumentosFaltantes.Add(documentosFaltantes);
                db.SaveChanges();
            var user = db.Users.Where(x => x.UserName.Equals(documentosFaltantes.Alumno.Usuario_id)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "DOCUMENTOS FALTANTES ESFU 1 : " + documentosFaltantes.DocumentosFaltantes_fecha, documentosFaltantes.DocumentosFaltantes_descripcion);
            }
            return RedirectToAction("Index");

        }

        // GET: Admins/DocumentosFaltantes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var documentosFaltantes = db.DocumentosFaltantes.Where(x => x.DocumentosFaltantes_id == id).Include("Alumno").FirstOrDefault();
            if (documentosFaltantes == null)
            {
                return HttpNotFound();
            }
            return View(documentosFaltantes);
        }

        // POST: Admins/DocumentosFaltantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentosFaltantes documentosFaltantes)
        {
                documentosFaltantes.Alumno = db.Alumno.Find(documentosFaltantes.Alumno.Alumno_id);
                db.Entry(documentosFaltantes).State = EntityState.Modified;
                db.SaveChanges();
            var user = db.Users.Where(x => x.UserName.Equals(documentosFaltantes.Alumno.Usuario_id)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(user.Email))
            {
                UserManager.SendEmail(user.Id, "DOCUMENTOS FALTANTES ESFU 1 : " + documentosFaltantes.DocumentosFaltantes_fecha, documentosFaltantes.DocumentosFaltantes_descripcion);
            }

            return RedirectToAction("Index");
            
        }

     
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentosFaltantes documentosFaltantes = db.DocumentosFaltantes.Find(id);
            if (documentosFaltantes == null)
            {
                return HttpNotFound();
            }
            db.DocumentosFaltantes.Remove(documentosFaltantes);
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
