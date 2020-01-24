using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using esfu1_controlEscolar.Areas.Admins.Models;
using esfu1_controlEscolar.Models;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AvisosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ImageServerController ImageServer = new ImageServerController();
        // GET: Admins/Avisos
        public ActionResult Index()
        {
            return View(db.Avisos.ToList());
        }

      

        // GET: Admins/Avisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Avisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Aviso_id,Aviso_descripcion,Aviso_fecha")] Avisos avisos, HttpPostedFileBase img)
        {
            if (ImageServer.ImgValid(img))
            {
                avisos.Aviso_img = ImageServer.AddImage(img, "/img/avisos/", this.Server);                
            }
            else
            {
                avisos.Aviso_img = "vacio";
            }
            avisos.Aviso_id = Guid.NewGuid();
            db.Avisos.Add(avisos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admins/Avisos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avisos avisos = db.Avisos.Find(id);
            if (avisos == null)
            {
                return HttpNotFound();
            }
            return View(avisos);
        }

        // POST: Admins/Avisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Avisos avisos, HttpPostedFileBase img)
        {
            if (img != null)
            {
                if (ImageServer.ImgValid(img))
                {
                    ImageServer.RemoveImage(avisos.Aviso_img, this.Server);
                    avisos.Aviso_img = ImageServer.AddImage(img, "/img/extraordinarios/", this.Server);
                }
            }
            db.Entry(avisos).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");            

        }


        public ActionResult DeleteConfirmed(Guid id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avisos avisos = db.Avisos.Find(id);
            if (avisos == null)
            {
                return HttpNotFound();
            }
            ImageServer.RemoveImage(avisos.Aviso_img, this.Server);
            db.Avisos.Remove(avisos);
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
