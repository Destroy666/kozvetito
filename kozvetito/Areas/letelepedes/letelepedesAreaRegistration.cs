using System.Web.Mvc;

namespace kozvetito.Areas.letelepedes
{
    public class letelepedesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "letelepedes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "letelepedes_default",
                "letelepedes/{controller}/{action}/{id}",
                new { controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}