using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("DocumentosFaltantes")]
    public class DocumentosFaltantes
    {
        [Key]
        public Guid DocumentosFaltantes_id { get; set; }

     
        public string DocumentosFaltantes_descripcion { get; set; }

        public DateTime DocumentosFaltantes_fecha { get; set; }

        [Required]
        public Alumno Alumno { get; set; }
    }
}