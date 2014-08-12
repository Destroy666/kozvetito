using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kozvetito.Areas.allas.DAL;

namespace kozvetito.Areas.allas.Controllers
{
    public class AllaslehetosegekController : BaseController
    {
        AllasContext db = new AllasContext();
        //
        // GET: /allas/Allaslehetosegek/
        public ActionResult Index()
        {
            return View(db.MunkaKereses.Include("SzotarMunkahelyPozicio"));
        }
	}
}