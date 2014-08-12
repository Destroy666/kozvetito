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
    public class Egyetem
    {
        [Key]
        public int Id { get; set; }

        public Guid UId { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "tanulmany_kezdete")]
        public int StartYear { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "tanulmany_vege")]
        public int EndYear { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "iskola")]
        public string Iskola { get; set; }

        [Required]
        [Display(ResourceType = typeof(MainLang), Name = "telepules")]
        public string Varos { get; set; }

        [Display(ResourceType = typeof(MainLang), Name = "szak")]
        public int SzakKod { get; set; }

        /// <summary>
        /// 1 - Középiskola
        /// 2 - Főiskola
        /// 3 - Egyetem
        /// </summary>
        [Required]
        [Display(Name = "Intézmény típusa")]
        public int Tipus { get; set; }

        public ICollection<Oneletrajz> Oneletrajzes { get; set; }

        [ForeignKey("SzakKod")]
        public virtual SzotarIskolaSzak SzotarIskolaSzak { get; set; }
    }
} 