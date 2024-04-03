using DnsClient;
using System.Net;

namespace DNS
{
    public static class ResolveDomainToIp
    {
        public static List<IPAddress> ResolveDomainToIpS(string domain, ILookupClient _lookupClient)
        {
            var result = _lookupClient.Query(domain, QueryType.A);
            var addressRecords = result.Answers.ARecords().Select(a => a.Address).ToList();

            if (addressRecords.Any())
            {
                return addressRecords;
            }
            else
            {
                throw new Exception("Nu s-au gasit adrese IP pentru domeniul specificat.");
            }
        }

        public static string getIp(string domain, ILookupClient _lookupClient)
        {
            try
            {
                 string ips= "";
               //domain = "www.facebook.com";
                List<IPAddress> ipAddresses = ResolveDomainToIpS(domain, _lookupClient);
                Console.WriteLine($"Adresele IP pentru domeniul {domain} sunt:");
                foreach (var ipAddress in ipAddresses)
                {
                    string var = ipAddress.ToString();
                    ips+= "\n" + var;
                }
                return ips;
            }
            catch
            {
                Console.ResetColor();
                return ("Nu s-au gasit adrese IP pentru domeniul specificat.");
            }
        }
    }
}
