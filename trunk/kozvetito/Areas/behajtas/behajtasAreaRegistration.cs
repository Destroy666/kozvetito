using System.Web.Mvc;

namespace kozvetito.Areas.behajtas
{
    public class behajtasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "behajtas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "behajtas_default",
                "behajtas/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}