using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kozvetito.Areas.allas.Models
{
    public class Oneletrajz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UId { get; set; }

        [Required(ErrorMessage = "A születésnapod megadása kötelező")]
        [Display(Name = "Születésnap")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Szuletesnap { get; set; }

        [Required(ErrorMessage = "Az irányítószám megadása kötelező")]
        [Display(Name = "Irányítószám")]
        [DataType(DataType.PostalCode)]
        public int IranyitoSzam { get; set; }

        [Required(ErrorMessage = "A település megadása kötelező")]
        [Display(Name = "Település")]
        public string Varos { get; set; }

        [Required(ErrorMessage = "Az utca, házszám megadása kötelező")]
        [Display(Name = "Utca, házszám")]
        public string UtcaHsz { get; set; }

        [Required(ErrorMessage = "A telefonszám megadása kötelező")]
        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        public virtual Egyetem Egyetem { get; set; }
        public virtual SzakmaiTapasztalat SzakmaiTapasztalat { get; set; }
        public virtual BeszeltNyelv BeszeltNyelv { get; set; }
        public virtual SzgepIsmFelh SzgepIsmFelh { get; set; }
        public virtual SzgepIsmAdmin SzgepIsmAdmin { get; set; }
        public virtual SzgepIsmProg SzgepIsmProg { get; set; }
        public virtual AdminEsKozgaz AdminEsKozgaz { get; set; }

        //Jogsi
        public bool A { get; set; }
        public bool B { get; set; }
        public bool C { get; set; }
        public bool D { get; set; }
        public bool E { get; set; }
        public bool T { get; set; }
    }
}