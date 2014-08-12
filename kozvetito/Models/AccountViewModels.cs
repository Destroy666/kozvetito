using System;
using System.ComponentModel.DataAnnotations;
using kozvetito.Areas.allas.Resource;

namespace kozvetito.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "felhasznalo_nev", ResourceType = typeof(MainLang))]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuális jelszó")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0}nak legalább {2} karakter hosszúnak kell lennie.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó mégegyszer")]
        [Compare("NewPassword", ErrorMessage = "A két jelszó nem egyezik meg.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "felhasznalo_nev", ResourceType = typeof(MainLang))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "jelszo", ResourceType = typeof(MainLang))]
        public string Password { get; set; }

        [Display(Name = "emlekezzen_ram", ResourceType = typeof(MainLang))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "felhasznalo_nev", ResourceType = typeof(MainLang))]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0}nak legalább {2} karakter hosszúnak kell lennie.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "jelszo", ResourceType = typeof(MainLang))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó mégegyszer")]
        [Compare("Password", ErrorMessage = "A két jelszó nem egyezik meg.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nem")]
        public bool Ferfi { get; set; }

        [Required]
        [Display(Name = "Név")]
        public string Nev { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Valós e-mail címet adjál meg.")]
        public string EMail { get; set; }

        [Display(Name = "Profilkép")]
        public string ProfilKep { get; set; }

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
        [Display(Name = "Elfogadom a felhasználási feltételeket")]
        public bool Felhaszn_feltetel { get; set; }
    }

    public class LoginRegisterViewModel
    {
        public LoginViewModel LoginModel { get; set; }
        public RegisterViewModel RegisterModel { get; set; }
    }
}
