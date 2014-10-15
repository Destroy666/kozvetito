using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using kozvetito.Areas.allas.DAL;
using kozvetito.Areas.allas.Models;
using kozvetito.Areas.allas.Models.Szotar;
using kozvetito.Models;
using Microsoft.AspNet.Identity;

namespace kozvetito.Areas.allas.Controllers
{
    public class OneletrajzController : BaseController
    {
        AllasContext db = new AllasContext();

        //
        // GET: /allas/Oneletrajz/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid uid = new Guid(User.Identity.GetUserId());
                var payok =
                    db.PayLogs.Where(x => x.UId == uid)
                        .Where(x => x.Csomag == "platina")
                        .Where(x => x.Elfogadva);

                if (payok.Count() != 0)
                {
                    return View(db.Oneletrajzes.Where(x => x.UId == uid).FirstOrDefault());    
                }
                else
                {
                    return RedirectToAction("Fizetes");
                }
            }
            else
            {
                return RedirectToAction("NemVagyBejelentkezve");
            }
        }

        public ActionResult NemVagyBejelentkezve()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            {
                return View();    
            }
            
        }

        public ActionResult Fizetes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Oneletrajz model)
        {
            model.UId = new Guid(User.Identity.GetUserId());

            var uid = new Guid(User.Identity.GetUserId());

            if (db.Oneletrajzes.Where(x => x.UId == uid).Count() == 0)
            {
                db.Oneletrajzes.Add(model);
            }
            else
            {
                var onelet = db.Oneletrajzes.Where(x => x.UId == uid).First();

                onelet.Szuletesnap = model.Szuletesnap;
                onelet.IranyitoSzam = model.IranyitoSzam;
                onelet.Varos = model.Varos;
                onelet.UtcaHsz = model.UtcaHsz;
                onelet.Telefon = model.Telefon;
                onelet.A = model.A;
                onelet.B = model.B;
                onelet.C = model.C;
                onelet.D = model.D;
                onelet.E = model.E;
                onelet.T = model.T;

            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Iskola()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.Egyetems.Where(x => x.UId == uid).OrderBy(x => x.StartYear));
        }

        public ActionResult UjIskola()
        {
            IEnumerable<SelectListItem> szak = new SelectList(db.SzotarIskolaSzaks.Distinct().OrderBy(x => x.Hu).ToList(), "Id", "Hu");
            ViewData["szak"] = szak;

            return PartialView();
        }

        [HttpPost]
        public ActionResult UjIskola(Egyetem model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.Egyetems.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        public ActionResult IskolaTorles(int id)
        {
            var del = db.Egyetems.First(x => x.Id == id);
            db.Egyetems.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Munkahely()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.SzakmaiTapasztalats.Where(x => x.UId == uid).OrderBy(x => x.StartYear));
        }

        public ActionResult UjMunkahely()
        {
            var beosztas = (from b in db.SzotarMunkahelyPozicios
                            select b).AsEnumerable().Select(x => new SelectListItem
                            {
                                Text = x.Hu,
                                Value = Convert.ToString(x.BeosztasKod)
                            }).OrderBy(x => x.Text);

            ViewBag.Beosztas = beosztas;

            return PartialView();
        }

        [HttpPost]
        public ActionResult UjMunkahely(SzakmaiTapasztalat model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.SzakmaiTapasztalats.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        public ActionResult MunkahelyTorles(int id)
        {
            var del = db.SzakmaiTapasztalats.First(x => x.Id == id);
            db.SzakmaiTapasztalats.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Nyelv()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.BeszeltNyelvs.Where(x => x.UId == uid).OrderBy(x => x.SzotarNyelv.Hu));
        }

        public ActionResult UjNyelv()
        {
            var nyelv = (from n in db.SzotarNyelvs
                         select n).AsEnumerable().Select(x => new SelectListItem
                         {
                             Text = x.Hu,
                             Value = Convert.ToString(x.NyelvKod)
                         }).OrderBy(x => x.Text);

            ViewBag.Nyelv = nyelv;

            var nyelvTudasszint = (from t in db.SzotarNyelvTudasszints
                                   select t).AsEnumerable().Select(x => new SelectListItem
                                   {
                                       Text = x.Hu,
                                       Value = Convert.ToString(x.Id)
                                   }).OrderBy(x => x.Value);

            ViewBag.NyelvTudasszint = nyelvTudasszint;

            return PartialView();
        }

        [HttpPost]
        public ActionResult UjNyelv(BeszeltNyelv model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.BeszeltNyelvs.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        public ActionResult NyelvTorles(int id)
        {
            var del = db.BeszeltNyelvs.First(x => x.Id == id);
            db.BeszeltNyelvs.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Szgep()
        {
            return PartialView();
        }

        public ActionResult FelhIsm()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.SzgepIsmFelhs.Where(x => x.UId == uid));
        }
        
        public ActionResult AdminIsm()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.SzgepIsmAdmins.Where(x => x.UId == uid));
        }
        
        public ActionResult ProgIsm()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.SzgepIsmProgs.Where(x => x.UId == uid));
        }

        public ActionResult UjSzgep()
        {
            return PartialView();
        }

        public ActionResult UjIsm()
        {
            var tudasszint = (from t in db.SzotarTudasszints
                              select t).AsEnumerable().Select(x => new SelectListItem
                              {
                                  Text = x.Hu,
                                  Value = Convert.ToString(x.TudasszintKod)
                              }).OrderBy(x => x.Value);

            ViewBag.Tudasszint = tudasszint;

            var szgepismfelh = (from s in db.SzotarSzgepIsmFelhs
                                select s).AsEnumerable().Select(x => new SelectListItem
                                {
                                    Text = x.Hu,
                                    Value = Convert.ToString(x.IsmeretKod)
                                }).OrderBy(x => x.Text);

            ViewBag.SzgepIsmFelh = szgepismfelh;

            return PartialView();
        }

        public ActionResult UjAdmin()
        {
            var tudasszint = (from t in db.SzotarTudasszints
                              select t).AsEnumerable().Select(x => new SelectListItem
                              {
                                  Text = x.Hu,
                                  Value = Convert.ToString(x.TudasszintKod)
                              }).OrderBy(x => x.Value);

            ViewBag.Tudasszint = tudasszint;

            var szgepismadmin = (from s in db.SzotarSzgepIsmAdmins
                                 select s).AsEnumerable().Select(x => new SelectListItem
                                 {
                                     Text = x.Hu,
                                     Value = Convert.ToString(x.IsmeretKod)
                                 }).OrderBy(x => x.Text);

            ViewBag.SzgepIsmAdmin = szgepismadmin;

            return PartialView();
        }

        public ActionResult UjProg()
        {
            var tudasszint = (from t in db.SzotarTudasszints
                              select t).AsEnumerable().Select(x => new SelectListItem
                              {
                                  Text = x.Hu,
                                  Value = Convert.ToString(x.TudasszintKod)
                              }).OrderBy(x => x.Value);

            ViewBag.Tudasszint = tudasszint;

            var szgepismprog = (from s in db.SzotarSzgepIsmProgs
                                select s).AsEnumerable().Select(x => new SelectListItem
                                {
                                    Text = x.Hu,
                                    Value = Convert.ToString(x.IsmeretKod)
                                }).OrderBy(x => x.Text);

            ViewBag.SzgepIsmProg = szgepismprog;

            return PartialView();
        }

        [HttpPost]
        public ActionResult UjFelhIsm(SzgepIsmFelh model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.SzgepIsmFelhs.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult UjAdmin(SzgepIsmAdmin model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.SzgepIsmAdmins.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });    
            }
            return Json(new { Success = true });
        }        
        
        [HttpPost]
        public ActionResult UjProg(SzgepIsmProg model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.SzgepIsmProgs.Add(model);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        public ActionResult FelhIsmTorles(int id)
        {
            var del = db.SzgepIsmFelhs.First(x => x.Id == id);
            db.SzgepIsmFelhs.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ProgTorles(int id)
        {
            var del = db.SzgepIsmProgs.First(x => x.Id == id);
            db.SzgepIsmProgs.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AdminTorles(int id)
        {
            var del = db.SzgepIsmAdmins.First(x => x.Id == id);
            db.SzgepIsmAdmins.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Kozgaz()
        {
            Guid uid = new Guid(User.Identity.GetUserId());
            return PartialView(db.AdminEsKozgazes.Where(x => x.UId == uid));
        }

        public ActionResult UjKozgaz()
        {
            var admin = (from a in db.SzotarAdminEsKozgazes
                         select a).AsEnumerable().Select(x => new SelectListItem
                         {
                             Text = x.Hu,
                             Value = Convert.ToString(x.Id)
                         }).OrderBy(x => x.Text);

            ViewBag.Admin = admin;

            var tudasszint = (from t in db.SzotarTudasszints
                              select t).AsEnumerable().Select(x => new SelectListItem
                              {
                                  Text = x.Hu,
                                  Value = Convert.ToString(x.TudasszintKod)
                              }).OrderBy(x => x.Value);

            ViewBag.Tudasszint = tudasszint;

            return PartialView();
        }

        [HttpPost]
        public ActionResult UjKozgaz(AdminEsKozgaz model)
        {
            try
            {
                model.UId = new Guid(User.Identity.GetUserId());
                db.AdminEsKozgazes.Add(model);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true });
        }

        public ActionResult KozgazTorles(int id)
        {
            var del = db.AdminEsKozgazes.First(x => x.Id == id);
            db.AdminEsKozgazes.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}