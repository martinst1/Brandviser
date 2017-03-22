using System.Web.Mvc;

namespace Brandviser.Web.Areas.Seller
{
    public class SellerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Seller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Seller_default",
                "Seller/{controller}/{action}/{name}",
                new { action = "Index", name = UrlParameter.Optional }
            );
        }
    }
}