using System;
using System.Linq;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Models;
using Brandviser.Web.Helpers.Contracts;
using Brandviser.Web.Properties;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace Brandviser.Web.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private readonly IUserService userService;
        private readonly IDomainService domainService;
        private readonly ILoggedInUser loggedInUser;

        public SellerController(IUserService userService, IDomainService domainService,
            ILoggedInUser loggedInUser)
        {
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();
            Guard.WhenArgument(loggedInUser, nameof(ILoggedInUser)).IsNull().Throw();

            this.userService = userService;
            this.domainService = domainService;
            this.loggedInUser = loggedInUser;
        }
        // GET: Seller/Seller
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            var userId = this.loggedInUser.GetUserId();

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
            return PartialView("_AddDomain");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDomain(AddDomainViewModel domainCandidate)
        {
            var userId = this.loggedInUser.GetUserId();

            if (!ModelState.IsValid)
            {
                return this.View(domainCandidate);
            }
            this.domainService.AddDomain(domainCandidate.Name, domainCandidate.Description, userId);
            TempData["Success"] = "Added successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult Domains()
        {
            return PartialView("_Domains");
        }

        public ActionResult Pending()
        {
            var userId = this.loggedInUser.GetUserId();

            var pendingDomains =
                this.domainService.GetSellerPendingDomainsByUserId(userId)
                .Select(d => new PartialDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending"
                }).ToList();

            return PartialView("_Pending", pendingDomains);
        }

        public ActionResult Rejected()
        {
            var userId = this.loggedInUser.GetUserId();

            var rejectedDomains =
                this.domainService.GetSellerRejectedDomainsByUserId(userId)
                .Select(d => new PartialDomainViewModel
                {
                    Name = d.Name,
                    Status = "Rejected"
                }).ToList();

            return PartialView("_Rejected", rejectedDomains);
        }

        public ActionResult Accepted()
        {
            var userId = this.loggedInUser.GetUserId();

            var acceptedDomains =
                this.domainService.GetSellerAcceptedDomainsByUserId(userId)
                .Select(d => new PartialPricedDomainViewModel
                {
                    Name = d.Name,
                    Status = "Accepted",
                    Price = d.Price
                }).ToList();

            return PartialView("_Accepted", acceptedDomains);
        }

        public ActionResult Published()
        {
            var userId = this.loggedInUser.GetUserId();

            var publishedDomains =
                this.domainService.GetSellerPublishedDomainsByUserId(userId)
                .Select(d => new PartialPricedDomainViewModel
                {
                    Name = d.Name,
                    Status = "Published",
                    Price = d.Price
                }).ToList();

            return PartialView("_Published", publishedDomains);
        }

        public ActionResult Sold()
        {
            var userId = this.loggedInUser.GetUserId();

            var soldDomains =
                this.domainService.GetSellerSoldDomainsByUserId(userId)
                .Select(d => new PartialPricedDatedDomainViewModel
                {
                    Name = d.Name,
                    Status = "Sold",
                    Price = d.Price,
                    SoldOn = (DateTime)d.SoldOn
                }).ToList();

            return PartialView("_Sold", soldDomains);
        }

        public ActionResult PendingDesign()
        {
            var userId = this.loggedInUser.GetUserId();

            var pendingDesignDomains =
                this.domainService.GetSellerPendingDesignDomainsByUserId(userId)
                .Select(d => new PartialDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending Design"
                }).ToList();

            return PartialView("_Pending", pendingDesignDomains);
        }

        public ActionResult Validate(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }

            var domain = this.domainService.GetDomainByName(name + ".com");

            if (domain == null)
            {
                return RedirectToAction("Index");
            }
            var validateDomainViewModel = new ValidateDomainViewModel()
            {
                Id = domain.Id,
                Name = domain.Name,
                VerificationCode = domain.VerificationCode.ToString().ToUpper()
            };

            return PartialView(validateDomainViewModel);
        }

        [HttpPost]
        public ActionResult ValidateDomain(string VerificationMethod,
            ValidateDomainViewModel validateDomainViewModel)
        {
            bool domainIsValid = false;

            if (VerificationMethod == "Nameserver")
            {
                domainIsValid = this.domainService.VerifyDomainNameNameservers
                    (validateDomainViewModel.Name + ".com",
                    Settings.Default.Nameserver1, Settings.Default.Nameserver2);
            }

            if (VerificationMethod == "TxtRecord")
            {
                domainIsValid = this.domainService.VerifyDomainNameByTxtRecord(validateDomainViewModel.Name + ".com");
            }

            if (domainIsValid)
            {
                this.domainService.SendDomainForLogoDesign(validateDomainViewModel.Name + ".com");
                TempData["Success"] = validateDomainViewModel.Name + " sent for logo design successfully!";
            }
            else
            {
                TempData["Error"] = validateDomainViewModel.Name + " failed " + VerificationMethod + " check!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }

            var domain = this.domainService.GetDomainByName(name + ".com");

            if (domain == null)
            {
                return RedirectToAction("Index");
            }

            var editDomainViewModel = new EditDomainViewModel()
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Price = domain.Price,
                OwnerCustomPrice = domain.OriginalOwnerCustomPrice
            };

            return PartialView(editDomainViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDomain(EditDomainViewModel editDomainViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", editDomainViewModel);
            }

            var price = editDomainViewModel.OwnerCustomPrice;
            var description = editDomainViewModel.Description;
            var name = editDomainViewModel.Name;

            this.domainService.EditDomainOwnerPriceAndDescription(name + ".com", price, description);

            TempData["Success"] = editDomainViewModel.Name + " edited successfully!";

            return RedirectToAction("Index");
        }
    }
}