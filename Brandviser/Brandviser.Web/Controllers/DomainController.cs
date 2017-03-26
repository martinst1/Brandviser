using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Models;
using Bytes2you.Validation;

namespace Brandviser.Web.Controllers
{
    public class DomainController : Controller
    {
        private readonly IDomainService domainService;

        public DomainController(IDomainService domainService)
        {
            Guard.WhenArgument(domainService, nameof(IDomainService)).IsNull().Throw();

            this.domainService = domainService;
        }
        // GET: Domain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null || id < 1)
            {
                return RedirectToAction("Index", "Home");
            }

            int domainId = (int)id;

            var domain = this.domainService.GetDomainById(domainId);

            if (domain == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var domainViewModel = new DomainDetailsViewModel()
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                SellerName = domain.User.FirstName + " " + domain.User.LastName,
                DesignerName = domain.Designer.FirstName + " " + domain.Designer.LastName,
                Price = domain.OriginalOwnerCustomPrice,
                LogoUrl = domain.LogoUrl,
                SellerId = domain.UserId,
                PostedOn = domain.UpdatedAt
            };

            return View(domainViewModel);
        }
    }
}