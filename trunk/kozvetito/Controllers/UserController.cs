using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kozvetito.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace kozvetito.Controllers
{
    public class UserController : Controller
    {
        public UserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await UserManager.FindAsync(model.UserName, model.Password);
                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);
                        return Json(new { Success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { Success = false, Errors = "Rendszerhiba.\nHa nem elöször jelenik meg ez az üzenet, kérem vegye fel a kapcsolatot az oldal készítőjével\nHiba azonosítása: ." + ErrorToHtml(ModelState) });
                }
                
            }

            // If we got this far, something failed, redisplay form
            return Json(new { Success = false, Errors = "Hibás felhasználónév vagy jelszó." });
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            model.ProfilKep = "default.jpg";

            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Nev = model.Nev,
                        Ferfi = model.Ferfi,
                        Profilkep =  model.ProfilKep,
                        felhaszn_feltetel = model.Felhaszn_feltetel
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return Json(new { Success = true });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    return Json(new { Success = false, Errors = ErrorToHtml(ModelState) });
                }
                
            }

            // If we got this far, something failed, redisplay form
            return Json(new { Success = false, Errors = ErrorToHtml(ModelState) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void resize(Image image, string outputFile)
        {
            int resizeMaxWidth = 800;
            int resizeMaxHeight = 600;

            double aspectRatio = 0;
            bool doResize = true;

            if (image.Width <= resizeMaxWidth && image.Height <= resizeMaxHeight)
            {
                doResize = false;
            }
            else
            {
                aspectRatio = image.Width/resizeMaxWidth;
                if (image.Height/aspectRatio > resizeMaxHeight)
                    aspectRatio = image.Height/resizeMaxHeight;
            }

            if (doResize)
            {
                int newWidth = (int)(image.Width/aspectRatio);
                int newHeight = (int) (image.Height/aspectRatio);
                using (var newImage = new Bitmap(newWidth,newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(image, new Rectangle(0,0,newWidth,newHeight));
                    var path = Path.Combine(Server.MapPath("~/Content/Profil"), outputFile);
                    image.Save(path);
                }

            }
        }

        private string ErrorToHtml(ModelStateDictionary state)
        {
            string totalError = null;

            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        totalError = totalError + error.ErrorMessage + Environment.NewLine;
                    }
                }
            }

            return totalError;
        }
	}
}