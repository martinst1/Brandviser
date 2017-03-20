using System;
using System.Linq;
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
        private IDomainService domainService;

        public SellerController(IUserService userService, IDomainService domainService)
        {
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();
            Guard.WhenArgument(userService, nameof(IDomainService)).IsNull().Throw();

            this.userService = userService;
            this.domainService = domainService;
        }
        // GET: Seller/Seller
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var user = this.userService.GetUserByStringId(userId);

            var sellerProfileBoxModel = new SellerProfileBoxStatsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Initials = user.FirstName[0].ToString() + user.LastName[0].ToString(),
                MemberSince = user.CreatedOn,
                Balance = user.Balance,
                BalanceInKUsd = Math.Round(user.Balance / 1000, 0) + "k",
                SubmittedDomains = user.SellerDomains.Count(),
                RejectedDomains = user.SellerDomains.Where(d => d.StatusId == 2).Count(),
                PendingDomains = user.SellerDomains.Where(d => d.StatusId == 1).Count(),
                PublishedDomains = user.SellerDomains.Where(d => d.StatusId == 4).Count(),
                SoldDomains = user.SellerDomains.Where(d => d.StatusId == 5).Count()
            };

            return View(sellerProfileBoxModel);
        }

        public ActionResult AddDomain()
        {
            return PartialView("AddDomain");
        }

        [HttpPost]
        public ActionResult AddDomain(AddDomainViewModel domainCandidate)
        {
            this.domainService.AddDomain(domainCandidate.Name, domainCandidate.Description, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }
    }
}