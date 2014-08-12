using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarNyelv
    {
        [Key]
        public int NyelvKod { get; set; }

        [Required]
        [Display(Name="Magyar")]
        public string Hu { get; set; }

        [Required]
        [Display(Name="Német")]
        public string De { get; set; }

        public ICollection<BeszeltNyelv> BeszeltNyelvs { get; set; }
    }
}