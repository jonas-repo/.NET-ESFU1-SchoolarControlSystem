using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Avisos")]
    public class Avisos
    {
        [Key]
        public Guid Aviso_id { get; set; }

        public string Aviso_img { get; set; }

        public DateTime Aviso_fecha { get; set; }

        public string Aviso_descripcion { get; set; }
    }
}