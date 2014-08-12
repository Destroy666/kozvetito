using System.Web.Mvc;

namespace kozvetito.Areas.oktatas
{
    public class oktatasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "oktatas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "oktatas_default",
                "oktatas/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}