using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kozvetito.Helpers;

namespace kozvetito.Areas.allas.Controllers
{
	public class FooldalController : BaseController
	{
		//
		// GET: /allas/Fooldal/
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult SetCulture(string culture)
		{
			culture = CultureHelper.GetImplementedCulture(culture);
			RouteData.Values["culture"] = culture;

			return RedirectToAction("Index");
		}

		public ActionResult Az_ausztriai_munkavallalas_titkai()
		{
			return View();
		}

		public ActionResult Hogyan_kerulheti_el_a_7_legnagyobb_hibat_ausztriai_munkavallalaskor()
		{
			return View();
		}

		public ActionResult Hogyan_regisztraljak_Milyen_csomagot_valasszak()
		{
			return View();
		}

		public ActionResult A_karrierhez_vezeto_ut_lepesei()
		{
			return View();
		}

		public ActionResult Nekunk_az_a_fontos_ami_onnek_fontos()
		{
			return View();
		}

		public ActionResult Welche_Anderungen_bringt()
		{
			return View();
		}

	    public ActionResult Ernsthafte_sanktionen_zu_rechnen_sein()
	    {
	        return View();
	    }

	    public ActionResult Die_restriktiven_massnahmen()
	    {
	        return View();
	    }
	}
}