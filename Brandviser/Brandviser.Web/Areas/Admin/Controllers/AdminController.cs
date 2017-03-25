using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Admin.Models;
using Brandviser.Web.Helpers.Contracts;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace Brandviser.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IDomainService domainService;
        private readonly ILoggedInUser loggedInUser;

        public AdminController(IUserService userService, IDomainService domainService,
            ILoggedInUser loggedInUser)
        {
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();
            Guard.WhenArgument(loggedInUser, nameof(ILoggedInUser)).IsNull().Throw();

            this.userService = userService;
            this.domainService = domainService;
            this.loggedInUser = loggedInUser;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            var userId = this.loggedInUser.GetUserId();
            var user = this.userService.GetUserByStringId(userId);

            var domainsForApprovalCount = this.domainService.GetAllDomainsPendingApproval().Count();

            var domainsForLogoApprovalCount = this.domainService.GetAllDomainsPendingLogoApproval().Count();

            var adminProfileBoxModel = new AdminProfileBoxStatsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Initials = user.FirstName[0].ToString() + user.LastName[0].ToString(),
                MemberSince = user.CreatedOn,
                DomainsPendingApproval = domainsForApprovalCount,
                DomainsPendingLogoApproval = domainsForLogoApprovalCount
            };

            return View(adminProfileBoxModel);
        }

        public ActionResult Domains()
        {
            return PartialView("_Domains");
        }

        public ActionResult PendingApproval()
        {
            var pendingApprovalDomains =
                this.domainService.GetAllDomainsPendingApproval()
                .Select(d => new PendingApprovalDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending Approval"
                }).ToList();

            return PartialView("_PendingApproval", pendingApprovalDomains);
        }

        public ActionResult ApprovePendingDomainIndex(string name)
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
            var approveDomainNameViewModel = new ApproveDomainNameViewModel()
            {
                Id = domain.Id,
                Name = domain.Name,
                SellerName = domain.User.FirstName + " " + domain.User.LastName,
                SellerUsername = domain.User.UserName
            };

            return PartialView("_ApprovePendingDomainIndex", approveDomainNameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApprovePendingDomain(string ApproveAction,
            ApproveDomainNameViewModel approveDomainNameViewModel)
        {
            if (ApproveAction == "Approve")
            {
                this.domainService.ApproveDomain(approveDomainNameViewModel.Name + ".com", approveDomainNameViewModel.Price);
                TempData["Success"] = approveDomainNameViewModel.Name + " approved!";
            }

            if (ApproveAction == "Reject")
            {
                this.domainService.RejectDomain(approveDomainNameViewModel.Name + ".com");
                TempData["Error"] = approveDomainNameViewModel.Name + " rejected!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult PendingLogoApproval()
        {
            var pendingLogoApprovalDomains =
                this.domainService.GetAllDomainsPendingLogoApproval()
                .Select(d => new PendingLogoApprovalDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending"
                }).ToList();

            return PartialView("_PendingLogoApproval", pendingLogoApprovalDomains);
        }

        public ActionResult ApprovePendingLogoDomainIndex(string name)
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
            var approveDomainLogoViewModel = new ApproveDomainLogoViewModel()
            {
                Id = domain.Id,
                Name = domain.Name,
                SellerName = domain.User.FirstName + " " + domain.User.LastName,
                SellerUsername = domain.User.UserName,
                LogoUrl = domain.LogoUrl
            };

            return PartialView("_ApprovePendingLogoDomainIndex", approveDomainLogoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApprovePendingLogo(string ApproveAction,
            ApproveDomainLogoViewModel approveDomainLogoViewModel)
        {
            if (ApproveAction == "Approve")
            {
                this.domainService.ApproveDomainLogo(approveDomainLogoViewModel.Name + ".com");
                TempData["Success"] = approveDomainLogoViewModel.Name + " logo approved!";
            }

            if (ApproveAction == "Reject")
            {
                this.domainService.RejectDomainLogo(approveDomainLogoViewModel.Name + ".com");
                TempData["Error"] = approveDomainLogoViewModel.Name + " logo rejected!";
            }

            return RedirectToAction("Index");
        }
    }
}