using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using kozvetito.Areas.allas.Models.Szotar;
using kozvetito.Areas.allas.Resource;

namespace kozvetito.Areas.allas.Models
{
    public class SzakmaiTapasztalat
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "munka_kezdet")]
        public int StartYear { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "munka_vege")]
        public int EndYear { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "munkahely")]
        public string Munkaado { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "munka_pozicio")]
        public int BeosztasKod { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }
        
        [ForeignKey("BeosztasKod")]
        public virtual SzotarMunkahelyPozicio SzotarMunkahelyPozicio { get; set; }
    }
}