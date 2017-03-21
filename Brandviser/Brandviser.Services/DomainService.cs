using System;
using Brandviser.Common.Contracts;
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
        private readonly IDateTimeProvider dateTimeProvider;

        public DomainService(IBrandviserData brandviserData, IDomainFactory domainFactory,
            IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(brandviserData, nameof(IBrandviserData)).IsNull().Throw();
            Guard.WhenArgument(domainFactory, nameof(IDomainFactory)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();


            this.domainFactory = domainFactory;
            this.brandviserData = brandviserData;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void AddDomain(string name, string description, string userId)
        {
            var createdAt = this.dateTimeProvider.GetCurrentTime();
            var domain = this.domainFactory.CreateDomain(userId, name, 1, description,
                createdAt);

            this.brandviserData.Domains.Add(domain);
            this.brandviserData.SaveChanges();
        }
    }
}
