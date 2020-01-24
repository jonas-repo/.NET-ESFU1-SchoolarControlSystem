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
    public class ExtraordinariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ImageServerController ImageServer = new ImageServerController();

        // GET: Admins/Extraordinarios
        public ActionResult Index()
        {
            return View(db.Extraordinarios.ToList());
        }

     
        // GET: Admins/Extraordinarios/Create
        public ActionResult Create(int? comp)
        {
            if(comp == 1)
            {
                ViewBag.NoEsImagen = "Porfavor ingresa una imagen con extension .jpg, .png";
            }
            return View();
        }

        // POST: Admins/Extraordinarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Extraordinario_id,Extraordinario_img,Extraordinario_fecha")] Extraordinarios extraordinarios, HttpPostedFileBase img)
        {
            if(ImageServer.ImgValid(img))
            {
                extraordinarios.Extraordinario_img = ImageServer.AddImage(img, "/img/extraordinarios/", this.Server);
                extraordinarios.Extraordinario_id = Guid.NewGuid();
                db.Extraordinarios.Add(extraordinarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create", new { comp = 1 });
            }

        }

        // GET: Admins/Extraordinarios/Edit/5
        public ActionResult Edit(Guid? id, int? comp)
        {
            if (comp == 1)
            {
                ViewBag.NoEsImagen = "Porfavor ingresa una imagen con extension .jpg, .png";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extraordinarios extraordinarios = db.Extraordinarios.Find(id);
            if (extraordinarios == null)
            {
                return HttpNotFound();
            }
            return View(extraordinarios);
        }

        // POST: Admins/Extraordinarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Extraordinario_id,Extraordinario_img,Extraordinario_fecha")] Extraordinarios extraordinarios, HttpPostedFileBase img)
        {
            if (img != null)
            {
                if (ImageServer.ImgValid(img))
                {
                    ImageServer.RemoveImage(extraordinarios.Extraordinario_img, this.Server);
                    extraordinarios.Extraordinario_img = ImageServer.AddImage(img, "/img/extraordinarios/", this.Server);
                }
                else
                {
                    return RedirectToAction("Edit", new { id = extraordinarios.Extraordinario_id, comp = 1 });
                }
            }         
            db.Entry(extraordinarios).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");           
        }

      
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extraordinarios extraordinarios = db.Extraordinarios.Find(id);
            if (extraordinarios == null)
            {
                return HttpNotFound();
            }
            ImageServer.RemoveImage(extraordinarios.Extraordinario_img, this.Server);
            db.Extraordinarios.Remove(extraordinarios);
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
