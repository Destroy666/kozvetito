using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kozvetito.Areas.konyveles.Controllers
{
    public class FooldalController : Controller
    {
        //
        // GET: /konyveles/Fooldal/
        public ActionResult Index()
        {
            return View();
        }
	}
}