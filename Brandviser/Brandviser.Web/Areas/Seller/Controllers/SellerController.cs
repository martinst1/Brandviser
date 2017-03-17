using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Models;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace Brandviser.Web.Areas.Seller.Controllers
{
    public class SellerController : Controller
    {
        private IUserService userService;

        public SellerController(IUserService userService)
        {
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();

            this.userService = userService;
        }
        // GET: Seller/Seller
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var user = this.userService.GetUserByGuidId(userId);

            var sellerProfileBoxModel = new SellerProfileBoxStatsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Initials = user.FirstName[0].ToString() + user.LastName[0].ToString(),
                // TODO: fix
                MemberFor = "0",
                Balance = user.Balance,
                BalanceInKUsd = Math.Round(user.Balance / 1000, 0) + "k",
                SubmittedDomains = user.Domains.Count(),
                RejectedDomains = user.Domains.Where(d => d.StatusId == 2).Count(),
                PendingDomains = user.Domains.Where(d => d.StatusId == 1).Count(),
                PublishedDomains = user.Domains.Where(d => d.StatusId == 4).Count(),
                SoldDomains = user.Domains.Where(d => d.StatusId == 5).Count()
            };

            return View(sellerProfileBoxModel);
        }
    }
}