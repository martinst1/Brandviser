namespace Brandviser.Common.Contracts
{
    public interface IWhois
    {
        string LookupDotComDomain(string domainName, int port, string whoisServer,
            string whoisServerLookupQueryPrefix, int responseBufferSizeInBytes);
    }
}
