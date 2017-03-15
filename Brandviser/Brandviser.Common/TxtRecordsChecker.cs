using System.Linq;
using System.Text;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using Brandviser.Common.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Common
{
    public class TxtRecordsChecker : ITxtRecordsChecker
    {
        public string GetRecords(string domain)
        {
            Guard.WhenArgument(domain, "Domain").IsNullOrEmpty().Throw();

            var parsedDomain = DomainName.Parse(domain);

            var messages = DnsClient.Default.Resolve(parsedDomain, RecordType.Txt);

            var records = messages.AnswerRecords.OfType<TxtRecord>();

            var stringBuilder = new StringBuilder();

            foreach (var r in records)
            {
                stringBuilder.AppendLine(r.ToString());
            }

            return stringBuilder.ToString().Trim();
        }
    }
}