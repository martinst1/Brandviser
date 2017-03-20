using System;
using Brandviser.Data.Contracts;
using Brandviser.Factories;
using Brandviser.Services.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Services
{
    public class DomainService : IDomainService
    {
        private readonly IBrandviserData brandviserData;
        private readonly IDomainFactory domainFactory;

        public DomainService(IBrandviserData brandviserData, IDomainFactory domainFactory)
        {
            Guard.WhenArgument(brandviserData, nameof(IBrandviserData)).IsNull().Throw();
            Guard.WhenArgument(brandviserData, nameof(IDomainFactory)).IsNull().Throw();

            this.domainFactory = domainFactory;
            this.brandviserData = brandviserData;
        }

        public void AddDomain(string name, string description, string userId)
        {
            var domain = this.domainFactory.CreateDomain(userId, name, 1, description, DateTime.Now);

            this.brandviserData.Domains.Add(domain);
            this.brandviserData.SaveChanges();
        }
    }
}
