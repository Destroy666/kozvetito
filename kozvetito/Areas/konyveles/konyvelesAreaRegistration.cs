using System.Web.Mvc;

namespace kozvetito.Areas.konyveles
{
    public class konyvelesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "konyveles";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "konyveles_default",
                "konyveles/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}