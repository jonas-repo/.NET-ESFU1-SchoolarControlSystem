using esfu1_controlEscolar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Alumno, Admin")]
    public class RoleAlumnoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

   
        public RoleAlumnoController()
        {
        }

        public RoleAlumnoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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






        // GET: Admins/RoleAlumno
        public ActionResult Reportes()
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var alumno = db.Alumno.Where(x => x.Usuario_id.Equals(logedUser.UserName)).FirstOrDefault();

            ViewBag.reportes = alumno.Reportes.ToList().OrderByDescending(x => x.Reporte_fecha).ToList();

            return View();
        }

        public ActionResult Citatorios()
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var alumno = db.Alumno.Where(x => x.Usuario_id.Equals(logedUser.UserName)).FirstOrDefault();

            ViewBag.Citatorios = alumno.Citatorios.ToList().OrderByDescending(x => x.Citatorio_fecha).ToList();

            return View();
        }

        public ActionResult DocumentosFaltantes()
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var alumno = db.Alumno.Where(x => x.Usuario_id.Equals(logedUser.UserName)).FirstOrDefault();

            ViewBag.docs = alumno.DocumentosFaltantes.ToList().OrderByDescending(x => x.DocumentosFaltantes_fecha).ToList();

            return View();
        }

        public ActionResult Calificaciones()
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var alumno = db.Alumno.Where(x => x.Usuario_id.Equals(logedUser.UserName)).FirstOrDefault();

            ViewBag.calificaciones = alumno.Calificaciones.ToList();

            return View();
        }

        public ActionResult Extraordinarios()
        {
            var extras = db.Extraordinarios.OrderByDescending(x => x.Extraordinario_fecha).ToList();
            ViewBag.extras = extras;
            return View();
        }

        public ActionResult Correo(int? comp)
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if(emailFormat.IsValid(logedUser.Email))
            {
                ViewBag.correo = logedUser.Email;
            }           
            if(comp == 1)
            {
                ViewBag.Success = "Registrado correctamente";
            }
            if(comp == 2)
            {
                ViewBag.Error = "Porfavor ingresa un correo electrónico valido";
            }
            if (comp == 3)
            {
                ViewBag.Error = "Este correo ya está siendo usado por otro usuario";
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Correo(string correo)
        {
            ApplicationUser logedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            EmailAddressAttribute emailFormat = new EmailAddressAttribute();
            if (emailFormat.IsValid(correo))
            {

                var emailNoRepetido = db.Users.Where(x => x.Email.Equals(correo)).FirstOrDefault();
                if(emailNoRepetido == null)
                {

                    logedUser.Email = correo;
                    var result = await UserManager.UpdateAsync(logedUser);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Correo", new { comp = 1 });
                    }
                    else
                    {
                        return RedirectToAction("Correo", new { comp = 2 });
                    }
                    

                }
                else
                {
                    return RedirectToAction("Correo", new { comp = 3 });
                }

                
            }
            else
            {
                return RedirectToAction("Correo", new { comp = 2 });
            }           
        }




    }
}