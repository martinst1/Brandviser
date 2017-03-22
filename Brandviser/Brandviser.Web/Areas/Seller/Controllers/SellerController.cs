using System;
using System.Linq;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Models;
using Brandviser.Web.Properties;
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
            return PartialView("_AddDomain");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDomain(AddDomainViewModel domainCandidate)
        {
            if (!ModelState.IsValid)
            {
                return this.View(domainCandidate);
            }
            this.domainService.AddDomain(domainCandidate.Name, domainCandidate.Description, User.Identity.GetUserId());
            TempData["Success"] = "Added successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult Domains()
        {
            return PartialView("_Domains");
        }

        public ActionResult Pending()
        {
            var pendingDomains =
                this.domainService.GetSellerPendingDomainsByUserId(User.Identity.GetUserId())
                .Select(d => new PartialDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending"
                }).ToList();

            return PartialView("_Pending", pendingDomains);
        }

        public ActionResult Rejected()
        {
            var rejectedDomains =
                this.domainService.GetSellerRejectedDomainsByUserId(User.Identity.GetUserId())
                .Select(d => new PartialDomainViewModel
                {
                    Name = d.Name,
                    Status = "Rejected"
                }).ToList();

            return PartialView("_Rejected", rejectedDomains);
        }

        public ActionResult Accepted()
        {
            var acceptedDomains =
                this.domainService.GetSellerAcceptedDomainsByUserId(User.Identity.GetUserId())
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
            var publishedDomains =
                this.domainService.GetSellerPublishedDomainsByUserId(User.Identity.GetUserId())
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
            var soldDomains =
                this.domainService.GetSellerPublishedDomainsByUserId(User.Identity.GetUserId())
                .Select(d => new PartialPricedDomainViewModel
                {
                    Name = d.Name,
                    Status = "Sold",
                    Price = d.Price
                }).ToList();

            return PartialView("_Sold", soldDomains);
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
                this.domainService.PublishDomain(validateDomainViewModel.Name + ".com");
                TempData["Success"] = validateDomainViewModel.Name + " published successfully!";
            }
            else
            {
                TempData["Error"] = validateDomainViewModel.Name + " failed " + VerificationMethod + " check!";
            }

            return RedirectToAction("Index");
        }
    }
}