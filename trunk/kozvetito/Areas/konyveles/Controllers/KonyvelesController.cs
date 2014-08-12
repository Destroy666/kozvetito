using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kozvetito.Areas.konyveles.Controllers
{
    public class KonyvelesController : Controller
    {
        //
        // GET: /konyveles/Konyveles/
        public ActionResult Index()
        {
            return View();
        }
	}
}