using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using kozvetito.Areas.allas.Models.Szotar;
using kozvetito.Areas.allas.Resource;
using kozvetito.Helpers;

namespace kozvetito.Areas.allas.Models
{
    public class MunkaKeres
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }
        
        [Required]
        [Display(Name = "telepules", ResourceType = typeof(MainLang))]
        public string Varos { get; set; }

        [Display(Name = "uzlet", ResourceType = typeof(MainLang))]
        public string Uzlet { get; set; }

        [Required]
        [Display(Name = "alk_szama", ResourceType = typeof(MainLang))]
        [DataType("Number")]
        public int Fo { get; set; }

        [Display(Name = "munka", ResourceType = typeof(MainLang))]
        public int BeosztasKod { get; set; }

        [ForeignKey("BeosztasKod")]
        public virtual SzotarMunkahelyPozicio SzotarMunkahelyPozicio { get; set; }
    }
}