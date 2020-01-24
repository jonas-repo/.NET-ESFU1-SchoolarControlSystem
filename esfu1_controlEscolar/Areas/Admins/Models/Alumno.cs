using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Alumno")]
    public class Alumno
    {
        [Key]
        public Guid Alumno_id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DisplayName("Ciclo escolar")]
        public string CicloEscolar { get; set; }

        [Required]
        public string Grado { get; set; }

        [Required]
        public string Grupo { get; set; }

        [Required]
        public string Curp { get; set; }

        public string Usuario_id { get; set; }

        public string Password { get; set; }
        
        public virtual ICollection<Calificacion> Calificaciones { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
        public virtual ICollection<Citatorio> Citatorios { get; set; }
        public virtual ICollection<DocumentosFaltantes> DocumentosFaltantes { get; set; }


    }
}