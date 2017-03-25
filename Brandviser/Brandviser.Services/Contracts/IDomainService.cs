using System.Collections.Generic;
using Brandviser.Data.Models;

namespace Brandviser.Services.Contracts
{
    public interface IDomainService
    {
        void AddDomain(string name, string description, string userId);

        IEnumerable<Domain> GetSellerPendingDomainsByUserId(string userId);

        IEnumerable<Domain> GetSellerRejectedDomainsByUserId(string userId);

        IEnumerable<Domain> GetSellerAcceptedDomainsByUserId(string userId);

        IEnumerable<Domain> GetSellerPublishedDomainsByUserId(string userId);

        IEnumerable<Domain> GetSellerPendingDesignDomainsByUserId(string userId);

        IEnumerable<Domain> GetSellerSoldDomainsByUserId(string userId);

        Domain GetDomainByName(string name);

        bool VerifyDomainNameNameservers(string name, string nameserver1, string nameserver2);

        bool VerifyDomainNameByTxtRecord(string name);

        void PublishDomain(string name);

        void EditDomainOwnerPriceAndDescription(string name, decimal? ownerPrice, string description);

        IEnumerable<Domain> GetAllDomainsPendingDesign();

        void UpdateDomainLogoPathAndDesignerId(string name, string logoPath, string designerId);

        IEnumerable<Domain> GetPendingApprovalDomainsSubmittedByDesigner(string designerId);

        IEnumerable<Domain> GetPublishedDomainsSubmittedByDesigner(string designerId);

        IEnumerable<Domain> GetAllDomainsPendingApproval();

        IEnumerable<Domain> GetAllDomainsPendingLogoApproval();

        void ApproveDomain(string name, decimal? price);

        void RejectDomain(string name);

        void ApproveDomainLogo(string name);

        void RejectDomainLogo(string name);

        void SendDomainForLogoDesign(string name);
    }
}
