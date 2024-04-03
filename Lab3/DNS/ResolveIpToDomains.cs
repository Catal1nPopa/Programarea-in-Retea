using DnsClient;
using System.Net;

namespace DNS
{
    public static class ResolveIpToDomains
    {
        public static List<string> ResolveIpToDomain(IPAddress ipAddress, ILookupClient _lookupClient)
        {
            var result = _lookupClient.QueryReverse(ipAddress);
            var domains = result.Answers.PtrRecords().Select(ptr => ptr.PtrDomainName.Value).ToList();

            if (domains.Any())
            {
                return domains;
            }
            else
            {
                throw new Exception("Nu s-au gasit domenii asociate adresei IP specificate.");
            }
        }

        public static string getDomain(string getIP, ILookupClient _lookupClient)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(getIP);
                List<string> domains = ResolveIpToDomain(ip, _lookupClient);
                Console.WriteLine($"Domeniile asociate adresei IP {ip} sunt:");
                foreach (var domainName in domains)
                {
                    Console.ResetColor();
                    return domainName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
                Console.ResetColor();
                return ex.ToString();
            }
            Console.ResetColor();
            return "";
        }
    }
}
