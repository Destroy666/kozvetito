using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarNyelvTudasszint
    {
        [Key]
        public int Id { get; set; }

        public string Hu { get; set; }

        public string De { get; set; }

        public ICollection<BeszeltNyelv> BeszeltNyelvs { get; set; }
    }
}