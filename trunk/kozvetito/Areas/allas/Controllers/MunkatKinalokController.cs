using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using kozvetito.Areas.allas.DAL;
using kozvetito.Areas.allas.Models;
using Microsoft.AspNet.Identity;

namespace kozvetito.Areas.allas.Controllers
{
    public class MunkatKinalokController : BaseController
    {
        AllasContext db = new AllasContext();

        //
        // GET: /allas/MunkatKinalok/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "hu")
                {
                    IEnumerable<SelectListItem> munka =
                    new SelectList(db.SzotarMunkahelyPozicios.Distinct().OrderBy(x => x.Hu).ToList(), "BeosztasKod", "Hu");
                    
                    ViewData["munka"] = munka;
                }
                else if (Thread.CurrentThread.CurrentCulture.Name == "de")
                {
                    IEnumerable<SelectListItem> munka =
                    new SelectList(db.SzotarMunkahelyPozicios.Distinct().OrderBy(x => x.De).ToList(), "BeosztasKod", "De");

                    ViewData["munka"] = munka;   
                }

                return View();    
            }
            {
                return RedirectToAction("NemVagyBejelentkezve");
            }
            
        }
        
        [HttpPost]
        public ActionResult Index(MunkaKeres model)
        {
            model.UId = new Guid(User.Identity.GetUserId());
            db.MunkaKereses.Add(model);
            db.SaveChanges();

            return RedirectToAction("Koszonjuk");
        }

        public ActionResult Koszonjuk()
        {
            return View();
        }

        public ActionResult NemVagyBejelentkezve()
        {
            return View();
        }
	}
}