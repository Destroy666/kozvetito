using System.Web.Mvc;

namespace kozvetito.Areas.partner
{
    public class partnerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "partner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "partner_default",
                "partner/{controller}/{action}/{id}",
                new { controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}