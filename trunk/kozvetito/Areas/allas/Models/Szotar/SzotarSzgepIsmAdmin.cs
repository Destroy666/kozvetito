using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarSzgepIsmAdmin
    {
        [Key]
        public int IsmeretKod { get; set; }

        [Required]
        [Display(Name = "Magyar")]
        public string Hu { get; set; }

        [Required]
        [Display(Name = "Német")]
        public string De { get; set; }

        public ICollection<SzgepIsmAdmin> SzgepIsmAdmins { get; set; }
    }
}