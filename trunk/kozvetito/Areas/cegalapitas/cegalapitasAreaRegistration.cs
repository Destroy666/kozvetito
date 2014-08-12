using System.Web.Mvc;

namespace kozvetito.Areas.cegalapitas
{
    public class cegalapitasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "cegalapitas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "cegalapitas_default",
                "cegalapitas/{controller}/{action}/{id}",
                new { controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}