using System.Web.Mvc;

namespace kozvetito.Areas.penzugy
{
    public class penzugyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "penzugy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "penzugy_default",
                "penzugy/{controller}/{action}/{id}",
                new {controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}