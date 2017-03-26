using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Buyer.Models;
using Brandviser.Web.Helpers.Contracts;
using Brandviser.Web.Models;
using Bytes2you.Validation;

namespace Brandviser.Web.Areas.Buyer.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private readonly ILoggedInUser loggedInUser;
        private readonly IDomainService domainService;
        private readonly IUserService userService;


        public BuyerController(ILoggedInUser loggedInUser, IDomainService domainService, IUserService userService)
        {
            Guard.WhenArgument(loggedInUser, nameof(ILoggedInUser)).IsNull().Throw();
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();

            this.loggedInUser = loggedInUser;
            this.domainService = domainService;
            this.userService = userService;

        }
        // GET: Buyer/Buyer
        public ActionResult Index()
        {
            var userId = this.loggedInUser.GetUserId();

            var user = this.userService.GetUserByStringId(userId);

            var buyerProfileBoxModel = new BuyerProfileBoxStatsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Initials = user.FirstName[0].ToString() + user.LastName[0].ToString(),
                MemberSince = user.CreatedOn,
                Balance = user.Balance,
                BalanceInKUsd = Math.Round(user.Balance / 1000, 0) + "k",
                OwnedDomains = this.domainService.GetBuyerOwnedDomainsByUserId(userId).Count()
            };

            return View(buyerProfileBoxModel);
        }

        public ActionResult Owned()
        {

            var userId = this.loggedInUser.GetUserId();

            var ownedDomains =
                this.domainService.GetBuyerOwnedDomainsByUserId(userId)
                .Select(d => new OwnedDomainViewModel
                {
                    Name = d.Name,
                    BoughtOn = (DateTime)d.SoldOn,
                    Price = d.OriginalOwnerCustomPrice,
                    Seller = d.User.FirstName + " " + d.User.LastName,
                    Designer = d.Designer.FirstName + " " + d.Designer.LastName
                }).ToList();

            return PartialView("_Owned", ownedDomains);
        }

        public ActionResult AddFunds()
        {
            return PartialView("_AddFunds");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFunds(AddFundsViewModel addFundsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(addFundsViewModel);
            }

            var userId = this.loggedInUser.GetUserId();
            var amount = addFundsViewModel.Amount;

            this.userService.TopUpUserBalance(userId, amount);
            TempData["Success"] = amount + " deposited successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyDomain(DomainDetailsViewModel domainDetails)
        {
            var buyerId = this.loggedInUser.GetUserId();
            var domainId = domainDetails.Id;
            var sellerId = domainDetails.SellerId;

            var buyerAlreadyOwnsDomain = this.domainService.CheckIfBuyerOwnsCertainDomain(domainId, buyerId);

            if (buyerAlreadyOwnsDomain)
            {
                TempData["Error"] = "You already own " + domainDetails.Name;
                return RedirectToAction("Details", "Domain", new { area = "", id = domainDetails.Id });
            }

            var amount = (decimal)domainDetails.Price;
            var buyerHasEnoughMoney = this.userService.CheckIfBuyerHasEnoughMoney(buyerId, amount);

            if (!buyerHasEnoughMoney)
            {
                TempData["Error"] = "Not enough funds! You can top up your account from the Dashboard";
                return RedirectToAction("Details", "Domain", new { area = "", id = domainDetails.Id });
            }

            this.userService.BuyDomain(buyerId, domainId);
            this.domainService.UpdateDomainToBought(domainId);
            this.userService.TransferAmountFromBuyerToSeller(buyerId, amount, sellerId);

            TempData["Success"] = "Horay! You bought " + domainDetails.Name + "!";

            return RedirectToAction("Index");
        }
    }
}