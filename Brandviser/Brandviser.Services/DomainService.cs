using System;
using System.Collections.Generic;
using System.Linq;
using Brandviser.Common.Constants;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Factories;
using Brandviser.Services.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Services
{
    public class DomainService : IDomainService
    {
        private readonly IBrandviserData brandviserData;
        private readonly IDomainFactory domainFactory;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IWhois whois;
        private readonly ITxtRecordsChecker txtRecordsChecker;


        public DomainService(IBrandviserData brandviserData, IDomainFactory domainFactory,
            IDateTimeProvider dateTimeProvider, IWhois whois, ITxtRecordsChecker txtRecordsChecker)
        {
            Guard.WhenArgument(brandviserData, nameof(IBrandviserData)).IsNull().Throw();
            Guard.WhenArgument(domainFactory, nameof(IDomainFactory)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();
            Guard.WhenArgument(whois, nameof(IWhois)).IsNull().Throw();
            Guard.WhenArgument(txtRecordsChecker, nameof(ITxtRecordsChecker)).IsNull().Throw();


            this.domainFactory = domainFactory;
            this.brandviserData = brandviserData;
            this.dateTimeProvider = dateTimeProvider;
            this.whois = whois;
            this.txtRecordsChecker = txtRecordsChecker;
        }

        public void AddDomain(string name, string description, string userId)
        {
            var createdAt = this.dateTimeProvider.GetCurrentTime();
            var domain = this.domainFactory.CreateDomain(userId, name, 1, description,
                createdAt);

            this.brandviserData.Domains.Add(domain);
            this.brandviserData.SaveChanges();
        }

        public IEnumerable<Domain> GetSellerPendingDomainsByUserId(string userId)
        {
            var pendingDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 1)
                .ToList();

            return pendingDomains;
        }

        public IEnumerable<Domain> GetSellerRejectedDomainsByUserId(string userId)
        {
            var rejectedDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 2)
                .ToList();

            return rejectedDomains;
        }

        public IEnumerable<Domain> GetSellerAcceptedDomainsByUserId(string userId)
        {
            var acceptedDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 3)
                .ToList();

            return acceptedDomains;
        }

        public IEnumerable<Domain> GetSellerPublishedDomainsByUserId(string userId)
        {
            var publishedDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 4)
                .ToList();

            return publishedDomains;
        }

        public IEnumerable<Domain> GetSellerPendingDesignDomainsByUserId(string userId)
        {
            var pendingDesignDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 6)
                .ToList();

            return pendingDesignDomains;
        }

        public IEnumerable<Domain> GetSellerSoldDomainsByUserId(string userId)
        {
            var soldDomains = this.brandviserData.Domains.All
                .Where(d => d.UserId == userId && d.StatusId == 5)
                .ToList();

            return soldDomains;
        }

        public Domain GetDomainByName(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            return domain;
        }

        public bool VerifyDomainNameNameservers(string name, string nameserver1, string nameserver2)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            var whoisInfo = whois.LookupDotComDomain(domain.Name,
                WhoisConstants.Port, WhoisConstants.WhoisServer, WhoisConstants.WhoisServerLookupQueryPrefix,
                WhoisConstants.RecommendedBufferSizeInBytes);

            if (whoisInfo.ToLower().Contains(nameserver1)
                || whoisInfo.ToLower().Contains(nameserver2))
            {
                return true;
            }

            return false;
        }

        public bool VerifyDomainNameByTxtRecord(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            var txtRecordsInfo = this.txtRecordsChecker.GetRecords(domain.Name);

            if (txtRecordsInfo.ToLower().Contains(domain.VerificationCode.ToString().ToLower()))
            {
                return true;
            }

            return false;
        }

        public void PublishDomain(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            domain.StatusId = 4;

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public void EditDomainOwnerPriceAndDescription(string name, decimal? ownerPrice, string description)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);
            domain.OriginalOwnerCustomPrice = ownerPrice;
            domain.Description = description;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public IEnumerable<Domain> GetAllDomainsPendingDesign()
        {
            var domains = this.brandviserData.Domains.All.Where(d => d.StatusId == 6).ToList();

            return domains;
        }

        public void UpdateDomainLogoPathAndDesignerId(string name, string logoPath, string designerId)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            domain.LogoUrl = logoPath;
            domain.DesignerId = designerId;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public IEnumerable<Domain> GetPendingApprovalDomainsSubmittedByDesigner(string designerId)
        {
            var domains = this.brandviserData.Domains
                .All.Where(d => d.DesignerId == designerId && d.StatusId == 6).ToList();

            return domains;
        }

        public IEnumerable<Domain> GetPublishedDomainsSubmittedByDesigner(string designerId)
        {
            var domains = this.brandviserData.Domains
                .All.Where(d => d.DesignerId == designerId && d.StatusId == 4).ToList();

            return domains;
        }

        public IEnumerable<Domain> GetAllDomainsPendingApproval()
        {
            var domainsPendingApproval = this.brandviserData.Domains.All.Where(d => d.StatusId == 1).ToList();

            return domainsPendingApproval;
        }

        public IEnumerable<Domain> GetAllDomainsPendingLogoApproval()
        {
            var domainsPendingLogoApproval = this.brandviserData
                .Domains.All.Where(d => d.StatusId == 6 && d.LogoUrl != null).ToList();

            return domainsPendingLogoApproval;
        }

        public void ApproveDomain(string name, decimal? price)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            domain.StatusId = 3;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();
            domain.Price = price;
            domain.OriginalOwnerCustomPrice = price;

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public void RejectDomain(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            domain.StatusId = 2;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public void ApproveDomainLogo(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            // goes to published
            domain.StatusId = 4;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public void RejectDomainLogo(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            domain.StatusId = 6;
            domain.LogoUrl = null;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public void SendDomainForLogoDesign(string name)
        {
            var domain = this.brandviserData.Domains.All.SingleOrDefault(d => d.Name == name);

            // approved domain moves to pending design status
            domain.StatusId = 6;
            domain.UpdatedAt = this.dateTimeProvider.GetCurrentTime();

            this.brandviserData.Domains.Update(domain);
            this.brandviserData.SaveChanges();
        }

        public IEnumerable<Domain> GetLatestEightPublishedDomains()
        {
            var domains = this.brandviserData.Domains
                .All.Where(d => d.StatusId == 4)
                .OrderByDescending(d => d.UpdatedAt)
                .Take(8)
                .ToList();

            return domains;
        }

        public Domain GetDomainById(int id)
        {
            var domain = this.brandviserData.Domains.GetById(id);

            return domain;
        }

        public bool CheckIfBuyerOwnsCertainDomain(int domainId, string buyerId)
        {
            var domain = this.brandviserData.Domains.GetById(domainId);

            var buyerOwnsDomain = domain.BuyerId == buyerId;

            return buyerOwnsDomain;
        }

        public void UpdateDomainToBought(int domainId)
        {
            var domain = this.brandviserData.Domains.GetById(domainId);

            domain.StatusId = 5;
            this.brandviserData.SaveChanges();
        }

        public IEnumerable<Domain> GetBuyerOwnedDomainsByUserId(string buyerId)
        {
            var domains = this.brandviserData.Domains.All.Where(d => d.BuyerId == buyerId).ToList();

            return domains;
        }
    }
}
