using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Reporte")]
    public class Reporte
    {
        [Key]
        public Guid Reporte_id { get; set; }

 
        public string Reporte_descripcion { get; set; }

        public DateTime Reporte_fecha { get; set; }

        [Required]
        public Alumno Alumno { get; set; }
    }
}