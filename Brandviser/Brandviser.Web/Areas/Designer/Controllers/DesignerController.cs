﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Designer.Models;
using Brandviser.Web.Helpers.Contracts;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace Brandviser.Web.Areas.Designer.Controllers
{
    [Authorize(Roles = "Designer")]
    public class DesignerController : Controller
    {
        private readonly IUserService userService;
        private readonly IDomainService domainService;
        private readonly ILoggedInUser loggedInUser;

        public DesignerController(IUserService userService, IDomainService domainService,
            ILoggedInUser loggedInUser)
        {
            Guard.WhenArgument(userService, nameof(IUserService)).IsNull().Throw();
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();
            Guard.WhenArgument(loggedInUser, nameof(ILoggedInUser)).IsNull().Throw();

            this.userService = userService;
            this.domainService = domainService;
            this.loggedInUser = loggedInUser;
        }
        // GET: Designer/Designer
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            var userId = this.loggedInUser.GetUserId();

            var user = this.userService.GetUserByStringId(userId);

            var designerProfileBoxModel = new DesignerProfileBoxStatsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Initials = user.FirstName[0].ToString() + user.LastName[0].ToString(),
                MemberSince = user.CreatedOn,
                Balance = user.Balance,
                BalanceInKUsd = Math.Round(user.Balance / 1000, 0) + "k",
                DomainsPendingLogoDesign = this.domainService.GetAllDomainsPendingDesign().Count(),
                Submitted = this.domainService.GetPendingApprovalDomainsSubmittedByDesigner(userId).Count(),
                Published = this.domainService.GetPublishedDomainsSubmittedByDesigner(userId).Count()
            };

            return View(designerProfileBoxModel);
        }

        public ActionResult Domains()
        {
            var pendingDesignDomains =
                this.domainService.GetAllDomainsPendingDesign()
                .Select(d => new PendingDesignDomainViewModel
                {
                    Name = d.Name,
                    Status = "Pending Design",
                    HasLogoUrl = d.LogoUrl == null ? true : false
                }).ToList();

            return PartialView("_Pending", pendingDesignDomains);
        }

        public ActionResult Propose(string name)
        {
            var submitLogoViewModel = new SubmitLogoViewModel()
            {
                Name = name
            };

            return PartialView("_Propose", submitLogoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendProposal(SubmitLogoViewModel submitLogoViewModel, HttpPostedFileBase file)
        {
            // to fix
            if (file == null)
            {
                return PartialView("_Propose", submitLogoViewModel);
            }

            string fileName = submitLogoViewModel.Name + "-candidate.png";

            string path = System.IO.Path.Combine(
                             Server.MapPath("~/Content/Domains/"), fileName);

            file.SaveAs(path);

            string logoPath = "~/Content/Domains/" + fileName;

            //var userId = User.Identity.GetUserId();
            var userId = this.loggedInUser.GetUserId();
            var domainName = submitLogoViewModel.Name + ".com";

            this.domainService.UpdateDomainLogoPathAndDesignerId(domainName, logoPath, userId);
            TempData["Success"] = "Logo proposal for " + submitLogoViewModel.Name + " sent successfully!";
            return RedirectToAction("Index");
        }
    }
}