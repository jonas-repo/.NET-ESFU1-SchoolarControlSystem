using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using esfu1_controlEscolar.Areas.Admins.Models;
using esfu1_controlEscolar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlumnoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AlumnoController()
        {
        }

        public AlumnoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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






        public PartialViewResult Index()
        {

            return PartialView(db.Alumno.ToList());
        }

        // GET: Admins/Alumno/Create
        public ActionResult Create(Guid? id_editar)
        {   
            if(id_editar != null)
            {
                ViewBag.Editar = "Si";
                var alumno = db.Alumno.Find(id_editar);
                return View(alumno);
            }
            return View();
        }

        // POST: Admins/Alumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public  ActionResult Create([Bind(Include = "Nombre,CicloEscolar,Grado,Grupo,Curp,ApellidoPaterno,ApellidoMaterno")] Alumno alumno, Guid? idAlumno)
        {



            if (ModelState.IsValid)
            {

                if(idAlumno != null )
                {
                    var alumnoEditado = db.Alumno.Find(idAlumno);
                    alumnoEditado.Nombre = alumno.Nombre;
                    alumnoEditado.CicloEscolar = alumno.CicloEscolar;
                    alumnoEditado.Curp = alumno.Curp;
                    alumnoEditado.Grado = alumno.Grado;
                    alumnoEditado.ApellidoPaterno = alumno.ApellidoPaterno;
                    alumnoEditado.ApellidoMaterno = alumno.ApellidoMaterno;
                    db.Entry(alumnoEditado).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Success = "Cambios guardados correctamente";
                    ModelState.Clear();
                    return View();
                }

                Random rnd = new Random();
                string card = rnd.Next(0, 2000).ToString("D4");

                string userName = alumno.ApellidoPaterno.Replace(" ", "") + alumno.ApellidoMaterno.Replace(" ", "") + card;
                Random generator = new Random();
                string pass = generator.Next(0, 999999).ToString("D6");

                var store = new UserStore<ApplicationUser>(db);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = userName, Email = userName };

                manager.Create(user, pass);
                manager.AddToRole(user.Id, "Alumno");

                alumno.Alumno_id = Guid.NewGuid();
                alumno.Password = pass;
                alumno.Usuario_id = userName;
                db.Alumno.Add(alumno);
                db.SaveChanges();
                ViewBag.Success = "Cambios guardados correctamente";
                ModelState.Clear();
                return View();

            }
            return View(alumno);
        }

 
       

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.FirstOrDefault(x => x.UserName.Equals(alumno.Usuario_id));

            if(user != null)
            {
                db.Users.Remove(user);
                db.Alumno.Remove(alumno);
                db.SaveChanges();
            }
            
           
            return RedirectToAction("Create");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }



}
