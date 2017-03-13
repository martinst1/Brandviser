using System.Text.RegularExpressions;

namespace Brandviser.Common.Constants
{
    public class WhoisConstants
    {
        public const int Port = 43;
        public const string WhoisServer = "whois.verisign-grs.com";
        public const string WhoisServerLookupQueryPrefix = "domain ";
        public const int RecommendedBufferSizeInBytes = 1024 * 4;

        // only letters, numbers and hyphen
        public const string RegexDomainPattern = "[a-z0-9-]";

        public const string DomainEndPattern = ".com";
        public const string ForbiddenDomainStartSymbol = "-";

        public const int MinimumDomainNameLength = 1;
        public const int MaximumDomainNameLength = 255;

        public const int TcpMinPort = 1;
        public const int TcpMaxPort = 65535;

        public const int MinimumResponseBufferSizeInBytes = 1024;
        public const int MaximumResponseBufferSizeInBytes = 1024 * 4;
    }
}
