using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarMunkahelyPozicio
    {
        [Key]
        public int BeosztasKod { get; set; }

        [Required]
        [Display(Name = "Magyar")]
        public string Hu { get; set; }

        [Required]
        [Display(Name = "Német")]
        public string De { get; set; }

        public ICollection<SzakmaiTapasztalat> SzakmaiTapasztalats { get; set; }

        public ICollection<MunkaKeres> MunkaKereses { get; set; }
    }
}