using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace kozvetito.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Név")]
        public string Nev { get; set; }

        [Required]
        [Display(Name = "Nem")]
        public bool Ferfi { get; set; }

        [Display(Name = "Profilkép")]
        public string Profilkep { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Valós e-mail címet adjál meg.")]
        public string email { get; set; }

        //[Display(Name = "Irányítószám")]
        //public int Iranyitoszam { get; set; }

        //[Display(Name = "Lakcím")]
        //public string Lakcim { get; set; }

        //[Display(Name = "Születési hely")]
        //public string SzulHely { get; set; }

        //[Display(Name = "Születési dátum")]
        //public DateTime SzulDatum { get; set; }

        //[Display(Name = "Anyja neve")]
        //public string AnyjaNeve { get; set; }

        //[Display(Name = "Önéletrajz")]
        //public bool Oneletrajz { get; set; }

        [Required]
        [Display(Name = "Felhasználási feltételek elfogadása")]
        public bool felhaszn_feltetel { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}