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
    public class SzgepIsmAdmin
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "admin_ism")]
        public int IsmeretKod { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "tudasszint")]
        public int TudasszintKod { get; set; }

        [ForeignKey("IsmeretKod")]
        public virtual SzotarSzgepIsmAdmin SzotarSzgepIsmAdmin { get; set; }

        [ForeignKey("TudasszintKod")]
        public virtual SzotarTudasszint SzotarTudasszint { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }
    }
}