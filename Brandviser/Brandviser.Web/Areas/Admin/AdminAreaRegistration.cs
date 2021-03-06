﻿using System.Web.Mvc;

namespace Brandviser.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{name}",
                new { action = "Index", name = UrlParameter.Optional }
            );
        }
    }
}