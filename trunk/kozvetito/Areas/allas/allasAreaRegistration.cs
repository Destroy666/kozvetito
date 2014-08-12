using System.Web.Mvc;
using kozvetito.Helpers;

namespace kozvetito.Areas.allas
{
    public class allasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "allas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "allas_default",
                "{culture}/allas/{controller}/{action}/{id}",
                new {culture = CultureHelper.GetDefaultCulture(), controller = "Fooldal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}