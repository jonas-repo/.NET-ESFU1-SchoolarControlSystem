using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    [Table("Extraordinarios")]
    public class Extraordinarios
    {
        [Key]
        public Guid Extraordinario_id { get; set; }
        public string Extraordinario_img { get; set; }
        public DateTime Extraordinario_fecha { get; set; }
    }
}