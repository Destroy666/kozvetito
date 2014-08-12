using System.Web.Mvc;

namespace kozvetito.Areas.weblap
{
    public class weblapAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "weblap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "weblap_default",
                "weblap/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}