using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Models;
using Bytes2you.Validation;

namespace Brandviser.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDomainService domainService;

        public HomeController(IDomainService domainService)
        {
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();

            this.domainService = domainService;
        }
        public ActionResult Index()
        {
            var latestDomains =
                this.domainService.GetLatestEightPublishedDomains()
                .Select(d => new DomainViewModel
                {
                    Name = d.Name,
                    LogoUrl = d.LogoUrl,
                    Price = d.OriginalOwnerCustomPrice,
                    Id = d.Id
                }).ToList();

            var homeViewModel = new SearchViewModel();

            homeViewModel.Domains = latestDomains;

            return View(homeViewModel);
        }
    }
}