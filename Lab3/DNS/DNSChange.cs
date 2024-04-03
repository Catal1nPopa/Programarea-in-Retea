using DnsClient;
using System.Net;
using System.Net.NetworkInformation;

namespace DNS
{
    public class DNSChange
    {
        private static ILookupClient _lookupClient = new LookupClient();
        public static string ChangeDnsServers(List<IPAddress> dnsServers)
        {
            if (CheckDNS(dnsServers))
            {
                var options = new LookupClientOptions(dnsServers.ToArray());
                _lookupClient = new LookupClient(options);
                return "Serverul DNS a fost schimbat cu succes.";
            }
            else
            {
                return "Nu s-a putut schimba serverul DNS. Verificati disponibilitatea serverului.";
            }
        }
        public static string ChangeDNS(string DNS1)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                List<IPAddress> newDnsServers = new List<IPAddress> { IPAddress.Parse(DNS1) };
                string result = ChangeDnsServers(newDnsServers);
                Console.ResetColor();
                return result;
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        public static bool CheckDNS(List<IPAddress> dnsServers)
        {
            foreach (var dnsServer in dnsServers)
            {
                try
                {
                    using (var ping = new Ping())
                    {
                        var reply = ping.Send(dnsServer);
                        if (reply.Status == IPStatus.Success)
                        {
                            return true;
                        }
                    }
                }
                catch (PingException)
                {
                    continue;
                }
            }
            return false;
        }

        public static void GetCurrentDnsServer()
        {
            Console.WriteLine("Serverul DNS curent:");
            foreach (var endpoint in _lookupClient.NameServers)
            {
                Console.WriteLine($"  Nume: {endpoint}");
                var ipAddress = endpoint.Address;
                Console.WriteLine($"  Adresa IP: {ipAddress}");
            }
            Console.WriteLine();
        }



        //Domain to IP
        public static string getIPDomain(string domain)
        {
            return ResolveDomainToIp.getIp(domain, _lookupClient);
        }

        //IP to Domain
        public static string getDomaiIPn(string ip)
        {
            return ResolveIpToDomains.getDomain(ip, _lookupClient);
        }

    }
}
