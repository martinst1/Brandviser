using System;
using Brandviser.Data.Models;

namespace Brandviser.Factories
{
    public interface IDomainFactory
    {
        Domain CreateDomain(string userId, string name, int statusId, string description, DateTime createdAt);
    }
}
