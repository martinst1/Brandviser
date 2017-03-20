namespace Brandviser.Services.Contracts
{
    public interface IDomainService
    {
        void AddDomain(string name, string description, string userId);
    }
}
