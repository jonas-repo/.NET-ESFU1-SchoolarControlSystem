using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esfu1_controlEscolar.Areas.Admins.Models
{
    public class GrupoCalificar
    {
        public string Materia { get; set; }

        [Required]
        public string Grupo { get; set; }

        [Required]
        public string CicloEscolar { get; set; }
    }
}