using System.Web.Mvc;

namespace kozvetito.Areas.ingatlan
{
    public class ingatlanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ingatlan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ingatlan_default",
                "ingatlan/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}