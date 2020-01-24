using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Citatorio")]
    public class Citatorio
    {
        [Key]
        public Guid Citatorio_id { get; set; }

     
        public string Citatorio_descripcion { get; set; }

        public DateTime Citatorio_fecha { get; set; }

        [Required]
        public Alumno Alumno { get; set; }
    }
}