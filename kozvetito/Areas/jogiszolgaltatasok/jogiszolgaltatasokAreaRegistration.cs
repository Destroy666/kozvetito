using System.Web.Mvc;

namespace kozvetito.Areas.jogiszolgaltatasok
{
    public class jogiszolgaltatasokAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "jogiszolgaltatasok";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "jogiszolgaltatasok_default",
                "jogiszolgaltatasok/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}