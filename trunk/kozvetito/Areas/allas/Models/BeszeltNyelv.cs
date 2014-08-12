using kozvetito.Areas.allas.Models.Szotar;
using kozvetito.Areas.allas.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models
{
    public class BeszeltNyelv
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Required]
        [Display(ResourceType=typeof(MainLang), Name="beszelt_nyelv")]
        public int NyelvKod { get; set; }

        [Required]
        [Display(ResourceType=typeof(MainLang), Name="tudasszint")]
        public int IsmeretKod { get; set; }

        [ForeignKey("NyelvKod")]
        public virtual SzotarNyelv SzotarNyelv { get; set; }

        [ForeignKey("IsmeretKod")]
        public virtual SzotarNyelvTudasszint SzotarNyelvTudasszint { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }
    }
}