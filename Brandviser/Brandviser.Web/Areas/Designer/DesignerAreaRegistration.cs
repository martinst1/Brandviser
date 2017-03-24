using System.Web.Mvc;

namespace Brandviser.Web.Areas.Designer
{
    public class DesignerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Designer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Designer_default",
                "Designer/{controller}/{action}/{name}",
                new { action = "Index", name = UrlParameter.Optional }
            );
        }
    }
}