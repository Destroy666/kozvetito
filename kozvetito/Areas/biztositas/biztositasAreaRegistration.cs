using System.Web.Mvc;

namespace kozvetito.Areas.biztositas
{
    public class biztositasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "biztositas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "biztositas_default",
                "biztositas/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}