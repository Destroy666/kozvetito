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
    public class SzgepIsmFelh
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "felhasz_ism")]
        public int IsmeretKod { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "tudasszint")]
        public int TudasszintKod { get; set; }

        [ForeignKey("IsmeretKod")]
        public virtual SzotarSzgepIsmFelh SzotarSzgepIsmFelh { get; set; }

        [ForeignKey("TudasszintKod")]
        public virtual SzotarTudasszint SzotarTudasszint { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }
    }
}