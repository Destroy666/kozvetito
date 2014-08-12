using System.Web.Mvc;

namespace kozvetito.Areas.forditas
{
    public class forditasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "forditas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "forditas_default",
                "forditas/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}