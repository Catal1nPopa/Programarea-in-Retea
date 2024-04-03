namespace DNS
{
    internal class Start
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                
                DNSChange.GetCurrentDnsServer();

                Console.WriteLine("Meniu:");
                Console.WriteLine("1. Schimbare DNS");
                Console.WriteLine("2. Obtinere IP pentru un domeniu");
                Console.WriteLine("3. Obtinere domeniu pentru o adresa IP");
                Console.WriteLine("0. Iesire\n");

                Console.Write("Alegeti o optiune: \n");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.WriteLine("Introdu DNS-ul nou:\n");
                        string DNS1 =  Console.ReadLine();
                        var dns = DNSChange.ChangeDNS(DNS1);
                        Console.WriteLine($"{dns}");
                        break;
                    case "2":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Write("Introduceti domeniul: \n");
                        string domain = Console.ReadLine();
                        var res = DNSChange.getIPDomain(domain);
                        Console.WriteLine($"{res}");
                        Console.ResetColor();
                        break;
                    case "3":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Write("Introduceti adresa IP: \n");
                        string ipAddressString = Console.ReadLine();
                        var ress = DNSChange.getDomaiIPn(ipAddressString);
                        Console.WriteLine($"{ress}");
                        Console.ResetColor();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida. Alegeti din nou.\n");
                        break;
                }
                Console.WriteLine("\n\n"); 
            }
        }
    }
}
