using System;
using System.Text;
using System.Text.RegularExpressions;
using Brandviser.Common.Constants;
using Brandviser.Common.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Common
{
    public class Whois : IWhois
    {
        private readonly ISocket socket;

        public Whois(ISocket socket)
        {
            Guard.WhenArgument(socket, nameof(ISocket)).IsNull().Throw();

            this.socket = socket;
        }

        public string LookupDotComDomain(string domainName, int port, string whoisServer,
            string whoisServerLookupQueryPrefix, int responseBufferSizeInBytes)
        {
            Guard.WhenArgument(domainName, "DomainName").IsNullOrEmpty().Throw();

            if (domainName.StartsWith(WhoisConstants.ForbiddenDomainStartSymbol))
            {
                throw new ArgumentException("Domain name cannot start with hyphen.");
            }

            if (!domainName.ToLower().EndsWith(WhoisConstants.DomainEndPattern))
            {
                throw new ArgumentException("Domain name should be a .com domain.");
            }

            var regex = new Regex(WhoisConstants.RegexDomainPattern);
            // remove ".com"
            if (!regex.IsMatch(domainName.ToLower().Substring(0, domainName.Length - 4)))
            {
                throw new ArgumentException("Domain should only contain letters, numbers or hyphen.");

            }

            Guard.WhenArgument(domainName.Length, "DomainName").IsLessThan(WhoisConstants.MinimumDomainNameLength).Throw();
            Guard.WhenArgument(domainName.Length, "DomainName").IsGreaterThan(WhoisConstants.MaximumDomainNameLength).Throw();

            Guard.WhenArgument(port, "Port").IsLessThan(WhoisConstants.TcpMinPort).Throw();
            Guard.WhenArgument(port, "Port").IsGreaterThan(WhoisConstants.TcpMaxPort).Throw();

            Guard.WhenArgument(whoisServer, "WhoisServer").IsNullOrEmpty().Throw();

            Guard.WhenArgument(whoisServerLookupQueryPrefix, "WhoisServerLookupQueryPrefix").IsNullOrEmpty().Throw();

            Guard.WhenArgument(responseBufferSizeInBytes, "ResponseBufferSizeInBytes").IsLessThan(WhoisConstants.MinimumResponseBufferSizeInBytes).Throw();
            Guard.WhenArgument(responseBufferSizeInBytes, "ResponseBufferSizeInBytes").IsGreaterThan(WhoisConstants.MaximumResponseBufferSizeInBytes).Throw();

            socket.Connect(whoisServer, port);

            byte[] query = Encoding.ASCII.GetBytes(whoisServerLookupQueryPrefix + domainName + Environment.NewLine);
            socket.Send(query);

            byte[] responseBytes = new byte[responseBufferSizeInBytes];
            socket.Receive(responseBytes);
            socket.Dispose();

            string humanReadableResponse = Encoding.UTF8.GetString(responseBytes);

            return humanReadableResponse;
        }
    }
}
