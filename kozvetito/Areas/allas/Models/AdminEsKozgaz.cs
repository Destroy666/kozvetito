using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using kozvetito.Areas.allas.Models.Szotar;

namespace kozvetito.Areas.allas.Models
{
    public class AdminEsKozgaz
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Display(Name = "Ismeret")]
        public int IsmeretKod { get; set; }

        [Display(Name = "Tudásszint")]
        public int TudasszintKod { get; set; }

        [ForeignKey("IsmeretKod")]
        public virtual SzotarAdminEsKozgaz SzotarAdminEsKozgaz { get; set; }

        [ForeignKey("TudasszintKod")]
        public virtual SzotarTudasszint SzotarTudasszint { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }
    }
}