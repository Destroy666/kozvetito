using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarTudasszint
    {
        [Key]
        public int TudasszintKod { get; set; }
        
        [Required]
        [Display(Name = "Magyar")]
        public string Hu { get; set; }

        [Required]
        [Display(Name = "Német")]
        public string De { get; set; }

        public ICollection<SzgepIsmAdmin> SzgepIsmAdmins { get; set; }
        public ICollection<SzgepIsmFelh> SzgepIsmFelhs { get; set; }
        public ICollection<SzgepIsmProg> SzgepIsmProgs { get; set; }
        public ICollection<AdminEsKozgaz> AdminEsKozgazes { get; set; }
    }
}