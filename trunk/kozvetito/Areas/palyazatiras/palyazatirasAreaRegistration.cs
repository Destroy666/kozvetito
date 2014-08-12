using System.Web.Mvc;

namespace kozvetito.Areas.palyazatiras
{
    public class palyazatirasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "palyazatiras";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "palyazatiras_default",
                "palyazatiras/{controller}/{action}/{id}",
                new { controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}