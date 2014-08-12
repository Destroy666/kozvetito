using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models.Szotar
{
    public class SzotarIskolaSzak
    {
        [Key]
        public int Id { get; set; }

        public string Hu { get; set; }

        public string De { get; set; }

        public ICollection<Egyetem> Egyetems { get; set; }
    }
}