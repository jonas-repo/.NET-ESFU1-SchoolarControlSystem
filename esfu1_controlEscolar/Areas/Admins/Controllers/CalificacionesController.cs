using esfu1_controlEscolar.Areas.Admins.Models;
using esfu1_controlEscolar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CalificacionesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? comp)
        {
            if(comp == 1)
            {
                ViewBag.Error = " Porfavor no dejes ningún campo sin seleccionar";
            }
            return View();
        }


        [HttpGet]
        public ActionResult RegistrarCalificacion(GrupoCalificar grupo)
        {
            
            if(ModelState.IsValid)
            {
                string[] grado = grupo.Materia.Split(',');
                string gradeNumber = grado[0];
                string materia = grado[1];


                //// obtener alumnos de grupo ciclo y grado
                var alumnos = db.Alumno.Where(
                    x => x.CicloEscolar.Equals(grupo.CicloEscolar)
                    && x.Grupo.Equals(grupo.Grupo) 
                    && x.Grado.Equals(gradeNumber)).ToList();

                if (alumnos.Count() == 0 || alumnos == null)
                {
                    ViewBag.vacio = "No hay alumnos registrados para estos parametros";
                    return View();
                }
                /////
                
                ///obtener la lista de alumnos con calificacion 
                
                List<AlumnosCalificados> alumnosCalificados = new List<AlumnosCalificados>();
                List<AlumnosCalificados> alumnosNoCalificados = new List<AlumnosCalificados>();

                var calificaciones = db.Calificacion.Where(x => x.Materia.Equals(materia));

                if(calificaciones != null)
                {
                    foreach (var alumno in alumnos)
                    {
                        var calificado = calificaciones.Where(x => x.Alumno.Alumno_id.Equals(alumno.Alumno_id)).FirstOrDefault();
                        if(calificado != null)
                        {
                            AlumnosCalificados alumn = new AlumnosCalificados
                            {
                                Alumno = alumno,
                                Calificacion = calificado
                            };
                            alumnosCalificados.Add(alumn);
                        }
                        else
                        {
                            AlumnosCalificados alumn = new AlumnosCalificados
                            {
                                Alumno = alumno,
                            };
                            alumnosNoCalificados.Add(alumn);
                        }

                    }
                    ViewBag.materia = materia;
                    ViewBag.CicloGrupo = grupo;
                    ViewBag.alumnosCalificados = alumnosCalificados;
                    ViewBag.alumnosNoCalificados = alumnosNoCalificados;
                    return View();
                }
                ////////
                ///
                foreach (var alumno in alumnos)
                {                   
                        AlumnosCalificados alumn = new AlumnosCalificados
                        {
                            Alumno = alumno,
                        };
                        alumnosNoCalificados.Add(alumn);                   
                }
                ViewBag.materia = materia;
                ViewBag.CicloGrupo = grupo;
                ViewBag.alumnosCalificados = alumnosCalificados;
                ViewBag.alumnosNoCalificados = alumnosNoCalificados;
                return View();
            }
            else
            {
                return RedirectToAction("Index",new { comp = 1});
            }
            
        }

        [HttpPost]
        public ActionResult RegistrarCalificacion(List<Calificacion> calificaciones, List<Calificacion> calificacionesEditar, GrupoCalificar grupo)
        {

            if(calificacionesEditar != null)
            {
                foreach (var calificacion in calificacionesEditar)
                {

                    float?[] calificacionBimestral = {
                        calificacion.PrimerBimestre,
                        calificacion.SegundoBimestre,
                        calificacion.TercerBimestre,
                        calificacion.CuartoBimestre,
                        calificacion.TercerBimestre
                    };
                    int?[] faltaBimestral =
                    {
                        calificacion.FaltasPrimerBimestre,
                        calificacion.FaltasSegundoBimestre,
                        calificacion.FaltasTercerBimestre,
                        calificacion.FaltasCuartoBimestre,
                        calificacion.FaltasQuintoBimestre
                    };
                    if (CheckCalificacionRegistrada(calificacionBimestral))
                    {
                        calificacion.TotalCalificacion = calificacionBimestral.Sum() / 5;
                    }
                    if (CheckFaltaRegistrada(faltaBimestral))
                    {
                        calificacion.TotalFaltas = faltaBimestral.Sum();
                    }
                    calificacion.Alumno = db.Alumno.Find(calificacion.Alumno.Alumno_id);
                    db.Entry(calificacion).State = EntityState.Modified;
                }
            }
            if(calificaciones != null )
            {
                //total faltas 
                //total calificacion
                //alumno
                
                foreach (var calificacion in calificaciones)
                {

                    float?[] calificacionBimestral = {
                        calificacion.PrimerBimestre,
                        calificacion.SegundoBimestre,
                        calificacion.TercerBimestre,
                        calificacion.CuartoBimestre,
                        calificacion.TercerBimestre
                    };
                    int?[] faltaBimestral =
                    {
                        calificacion.FaltasPrimerBimestre,
                        calificacion.FaltasSegundoBimestre,
                        calificacion.FaltasTercerBimestre,
                        calificacion.FaltasCuartoBimestre,
                        calificacion.FaltasQuintoBimestre
                    };
                    if(CheckCalificacionRegistrada(calificacionBimestral))
                    {
                        calificacion.TotalCalificacion = calificacionBimestral.Sum() / 5;
                    }
                    if(CheckFaltaRegistrada(faltaBimestral))
                    {
                        calificacion.TotalFaltas = faltaBimestral.Sum();
                    }
                    calificacion.Alumno = db.Alumno.Find(calificacion.Alumno.Alumno_id);
                    calificacion.Calificacion_id = Guid.NewGuid();
                    db.Calificacion.Add(calificacion);                    
                }
                
            }
            db.SaveChanges();
            //////
            ///
            string[] grado = grupo.Materia.Split(',');
            string gradeNumber = grado[0];
            string materia = grado[1];


            //// obtener alumnos de grupo ciclo y grado
            var alumnos = db.Alumno.Where(
                x => x.CicloEscolar.Equals(grupo.CicloEscolar)
                && x.Grupo.Equals(grupo.Grupo)
                && x.Grado.Equals(gradeNumber)).ToList();

            if (alumnos.Count() == 0 || alumnos == null)
            {
                ViewBag.vacio = "No hay alumnos registrados para estos parametros";
                return View();
            }
            /////

            ///obtener la lista de alumnos con calificacion 

            List<AlumnosCalificados> alumnosCalificados = new List<AlumnosCalificados>();
            List<AlumnosCalificados> alumnosNoCalificados = new List<AlumnosCalificados>();

            var calificaciones1 = db.Calificacion.Where(x => x.Materia.Equals(materia));

            if (calificaciones1 != null)
            {
                foreach (var alumno in alumnos)
                {
                    var calificado = calificaciones1.Where(x => x.Alumno.Alumno_id.Equals(alumno.Alumno_id)).FirstOrDefault();
                    if (calificado != null)
                    {
                        AlumnosCalificados alumn = new AlumnosCalificados
                        {
                            Alumno = alumno,
                            Calificacion = calificado
                        };
                        alumnosCalificados.Add(alumn);
                    }
                    else
                    {
                        AlumnosCalificados alumn = new AlumnosCalificados
                        {
                            Alumno = alumno,
                        };
                        alumnosNoCalificados.Add(alumn);
                    }

                }
                ViewBag.materia = materia;
                ViewBag.CicloGrupo = grupo;
                ViewBag.alumnosCalificados = alumnosCalificados;
                ViewBag.alumnosNoCalificados = alumnosNoCalificados;
                return View();
            }
            ////////
            ///
            foreach (var alumno in alumnos)
            {
                AlumnosCalificados alumn = new AlumnosCalificados
                {
                    Alumno = alumno,
                };
                alumnosNoCalificados.Add(alumn);
            }
            ViewBag.materia = materia;
            ViewBag.CicloGrupo = grupo;
            ViewBag.alumnosCalificados = alumnosCalificados;
            ViewBag.alumnosNoCalificados = alumnosNoCalificados;
            return View();
        }

        public bool CheckCalificacionRegistrada(float?[] calificacion)
        {
            foreach (var registro in calificacion)
            {
                if (registro == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckFaltaRegistrada(int?[] faltas)
        {
            foreach (var registro in faltas)
            {
                if (registro == null)
                {
                    return false;
                }
            }
            return true;
        }

    }
}