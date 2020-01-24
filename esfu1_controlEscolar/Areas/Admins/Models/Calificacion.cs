using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Calificacion")]
    public class Calificacion
    {
        [Key]
        public Guid Calificacion_id { get; set; }


        public string Materia { get; set; }

        public string Nombre { get; set; }

        public float? PrimerBimestre { get; set; }

        public float? SegundoBimestre { get; set; }

        public float? TercerBimestre { get; set; }

        public float? CuartoBimestre { get; set; }

        public float? QuintoBimestre { get; set; }

        public int? FaltasPrimerBimestre { get; set; }

        public int? FaltasSegundoBimestre { get; set; }

        public int? FaltasTercerBimestre { get; set; }

        public int? FaltasCuartoBimestre { get; set; }

        public int? FaltasQuintoBimestre { get; set; }

        public int? TotalFaltas { get; set; }

        public float? TotalCalificacion { get; set; }


        [Required]
        public virtual Alumno Alumno { get; set; }

    }
}