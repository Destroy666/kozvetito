using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using kozvetito.Areas.allas.DAL;
using kozvetito.Areas.allas.Models;
using kozvetito.Models;
using kozvetito.PayPlaza;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace kozvetito.Areas.allas.Controllers
{
    public class FizetesController : BaseController
    {
        AllasContext db = new AllasContext();

        //
        // GET: /allas/Fizetes/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bankkartya(string eszkoz)
        {
            if (eszkoz == "bankcard")
            {
                ViewBag.Header = "Bankkártyás fizetés";
            }
            else if (eszkoz == "onlinetransfer")
            {
                ViewBag.Header = "Online banki átutalás";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Bankkartya(string csomag, string tel, string email, bool kellszamla, string nev, string adoszam, string kozadoszam, string orszag, string telepules, string irszam, string kozternev, string kozterjell, string hazszam, string lepcsohaz, string emelet, string ajto, string eszkoz)
        {
            PayPlazaServiceV5SoapClient client = new PayPlazaServiceV5SoapClient();

            string trid = client.GetTridStr();
            string merchantCode = "voxiteszt"; //partnerkód, végelegesítéskor át kell írni.
            string userEmail = email;
            string userPhone = tel;
            string displayname = string.Empty;
            string productcode = string.Empty;
            string netunitprice = string.Empty;

            switch (csomag)
            {
                case "gold":
                    displayname = "Exclusive Team - Gold csomag";
                    productcode = "voxiteszt1"; //átírni!!!
                    netunitprice = "1575";
                    break;

                case "platina":
                    displayname = "Exclusive Team - Platina csomag";
                    productcode = "voxiteszt2"; //átírni!!!
                    netunitprice = "3543";
                    break;
            }

            XElement product = new XElement("products",
                new XElement("product",
                    new XElement("displayname", displayname),
                    new XElement("productcode", productcode),
                    new XElement("netunitprice", netunitprice),
                    new XElement("unit", "db"),
                    new XElement("quantity", "1")
                    )
                );


            string productsXml = product.ToString();
            string paymentMethod = eszkoz;
            string returnUrl = "http://exclusiveteam.net/allas/fizetes/koszonjuk?trid=" + trid;
            string notificationUrl = "http://exclusiveteam.net/allas/fizetes/feldolgozas?trid=" + trid;
            string currency = "HUF";
            string language = "hu";
            string shopRemark;
            switch (csomag)
            {
                case "gold":
                    shopRemark = "Exclusive Team - Gold csomag";
                    break;

                case "platina":
                    shopRemark = "Exclusive Team - Platina csomag";
                    break;

                default:
                    shopRemark = "Exclusive Team - Vásárlás";
                    break;
            }

            string invoiceForXml = string.Empty; //számlázás

            XElement szamla = new XElement("vevo",
                new XElement("nev",nev),
                new XElement("adoszam",adoszam),
                new XElement("kozadoszam",kozadoszam),
                new XElement("cim",
                    new XElement("orszag",orszag),
                    new XElement("telepules",telepules),
                    new XElement("irszam",irszam),
                    new XElement("kozternev",kozternev),
                    new XElement("kozterjell",kozterjell),
                    new XElement("hazszam",hazszam),
                    new XElement("lepcsohaz",lepcsohaz),
                    new XElement("emelet",emelet),
                    new XElement("ajto",ajto)
                    )
                );

            if (kellszamla)
            {
                invoiceForXml = szamla.ToString(); 
            }
            
            bool needInvoice = kellszamla;
            bool isTestMode = true;

            PayLog log = new PayLog();
            log.UId = new Guid(User.Identity.GetUserId());
            log.Trid = trid;
            log.UserEmail = userEmail;
            log.UserPhone = userPhone;
            log.Csomag = csomag;
            log.ProductXml = product;
            log.PaymentMethod = paymentMethod;
            log.Currency = currency;
            log.SzamlaXml = szamla;
            log.DateTimeGMT = DateTime.UtcNow;
            log.Elfogadva = false;
            
            db.PayLogs.Add(log);
            db.SaveChanges();

            XmlNode pR = client.PaymentRequest(trid, merchantCode, userEmail, userPhone, productsXml,
                paymentMethod, returnUrl, notificationUrl, currency, language, shopRemark, invoiceForXml, needInvoice,
                isTestMode);

            int ResultCode = Convert.ToInt32(pR["ResultCode"].InnerText);
            string Description = pR["Description"].InnerText;
            string PaymentUrl = pR["PaymentUrl"].InnerText;
            string CreatedOn = pR["CreatedOn"].InnerText;

            var update = db.PayLogs.FirstOrDefault(x => x.Trid == trid);
            if (update != null)
            {
                update.PayRequestXml = new XElement(pR.Name,pR.InnerXml);
                db.SaveChanges();

                if (ResultCode == 0)
                {
                    return Redirect(PaymentUrl);
                }   
            }

            return View();
        }

        public ActionResult Feldolgozas(string trid)
        {
            try
            {
                PayPlazaServiceV5SoapClient client = new PayPlazaServiceV5SoapClient();

                XmlNode d = client.GetPaymentDetails(trid);

                string CheckResultCode = d["CheckResultCode"].InnerText;
                string PaidResultCode = d["PaidResultCode"].InnerText;
                string IsTestPayment = d["IsTestPayment"].InnerText;
                string Amount = d["Amount"].InnerText;
                string PaidAmount = d["PaidAmount"].InnerText;
                string Currency = d["Currency"].InnerText;
                string PaidWith = d["PaidWith"].InnerText;
                string InvoiceUrl = d["InvoiceUrl"].InnerText;
                string StartedOn = d["StartedOn"].InnerText;
                string PaidOn = d["PaidOn"].InnerText;

                var update = db.PayLogs.FirstOrDefault(x => x.Trid == trid);
                update.PaymentDetailsXml = new XElement(d.Name, d.InnerXml);
                if ((Convert.ToInt32(CheckResultCode) == 0) && (Convert.ToInt32(PaidResultCode) == 0))
                {
                    update.Elfogadva = true;

                    MailMessage mail = new MailMessage();

                    var uid = db.PayLogs.Where(x => x.Trid == trid).Select(x => x.UId).First();
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var user = userManager.FindById(uid.ToString());
                    string email = user.email;

                    mail.To.Add(email);
                    mail.From = new MailAddress("noreply@exclusiveteam.net");
                    mail.Subject = "Exclusive Team: Sikeres vásárlás";
                    string Body =
                        "<h3>Köszönjük a várlást!</h3><p>Számla információk:<br>Fizetés helye: http://exclusiveteam.net<br>Fizetett összeg: " +
                        PaidAmount + " " + Currency +
                        "<br>Fizetett szolgáltatás megnevezése: csomag vásárlás<br>Tranzakció azonosító: " + trid +
                        "<br>Dátum: " + PaidOn + "</p><p>Exclusive Team</p>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.Port = 25;
                    smtp.Credentials = new NetworkCredential("noreply@exclusiveteam.net", "admin666");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                throw;
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.RequestTimeout);
        }

        public ActionResult Koszonjuk(string trid)
        {
            PayPlazaServiceV5SoapClient client = new PayPlazaServiceV5SoapClient();

            XmlNode data = client.GetPaymentDetails(trid);

            string CheckResultCode = data["CheckResultCode"].InnerText;
            string PaidResultCode = data["PaidResultCode"].InnerText;

            if (Convert.ToInt32(CheckResultCode) == 0)
            {
                switch (Convert.ToInt32(PaidResultCode))
                {
                    case 0:
                        ViewBag.Header = "Köszönjük vásárlását";
                        ViewBag.Content = "Mostantól már elérhető az őn által választott szolgáltatás.";
                        break;

                    case 602:
                        ViewBag.Header = "Lezáratlan tranzakció";
                        ViewBag.Content =
                            "A banktól adott időpontban nem lehet lekérdezni a fizetés eredményét. A rendszer 24 órán belül rendszeres időközönként újra megpróbál kommunikálni a bankkal. Ha ez sikeressen megtörtént, akkor e-mailben értesítést kap a regisztrációkor megadott e-mail címére, a sikeres tranzakciórol. <br><br>Amíg nem zárul le a tranzakció, kérjük ne indítson új fizetést!";
                        break;

                    case 900:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A tranzakció nem jött létre.";
                        break;

                    case 1027:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "American Express kártyával történő fizetés visszautasításra került.";
                        break;

                    case 1030:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya blokkolt.";
                        break;

                    case 1031:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártyaszám érvénytelen.";
                        break;

                    case 1032:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártyaszám érvénytelen.";
                        break;

                    case 1033:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya lejárt.";
                        break;

                    case 1034:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya letiltott.";
                        break;

                    case 1035:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya elveszett kártya.";
                        break;

                    case 1036:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya nem aktív.";
                        break;

                    case 1037:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártyaadatok hibásak.";
                        break;

                    case 10387:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártyaadatok hibásak, vagy nincs elég fedezet.";
                        break;

                    case 1039:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya az üzletági követelményeknek nem felel meg.";
                        break;

                    case 1040:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártyaszám ismeretlen.";
                        break;

                    case 1041:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya terhelése nem lehetséges.";
                        break;

                    case 1042:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A megadott kártya terhelése a megadott összeggel nem lehetséges (jellemzően vásárlási limittúllépés miatt).";
                        break;

                    case 1043:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "Érvénytelen összegű vásárlás";
                        break;

                    case 1044:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "Hibás felhasználó azonosító.";
                        break;

                    case 1045:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "Kapcsolat jellegű hiba.";
                        break;

                    case 1046:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A fizetés vissza lett utasítva.";
                        break;

                    case 1047:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "Idő túllépés";
                        break;

                    case 1048:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A vásárló visszalépett a fizetéstől.";
                        break;

                    case 1049:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "Vevőoldali időtúllépés, amennyiben a fizetés 10 percen bellül nem történt meg.";
                        break;

                    case 1050:
                        ViewBag.Header = "Folyamatba lévő vásárlás";
                        ViewBag.Content = "A felhasználó még a banki fizetést bonyolítja.";
                        break;

                    case 1051:
                        ViewBag.Header = "Rendszer hiba";
                        ViewBag.Content = "A felhasználóazonosító nem létezik, nincs ilyen regisztrált felhasználó.";
                        break;

                    case 1052:
                        ViewBag.Header = "Sikertelen vásárlás";
                        ViewBag.Content = "A kétlépcsős fizetési tranzakció elutasítással zárult.";
                        break;

                    default:
                        ViewBag.Header = "Ismeretlen hiba";
                        ViewBag.Content = "Ismeretlen hiba következett be a fizetés során.";
                        break;
                }
            }

            return View();
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("{0}: {1}", DateTime.UtcNow.AddHours(1), logMessage);
        }
	}
}