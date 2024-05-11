using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine(
                "Introduceti zona geografica (in format \"GMT+X\" sau \"GMT-X\", unde X este o cifra intre 0 si 11):");
            string input = Console.ReadLine();

            int gmtOffset;
            if (int.TryParse(input.Replace("GMT", ""), out gmtOffset))
            {
                if (gmtOffset >= -11 && gmtOffset <= 12)
                {
                    DateTime timeInGMT = GetTimeForGMTZone(gmtOffset);
                    Console.WriteLine($"Ora exacta in zona orara {input} este: {timeInGMT}");
                }
                else
                {
                    Console.WriteLine("Introduceti o cifra valida intre -11 si 12 pentru zona orara.");
                }
            }
            else
            {
                Console.WriteLine(
                    "Introduceti o valoare valida in formatul \"GMT+X\" sau \"GMT-X\", unde X este o cifra intre 0 si 11.");
            }
        }
    }

    static DateTime GetTimeForGMTZone(int gmtOffset)
    {
        var ntpServer = "md.pool.ntp.org";
        var ntpData = new byte[48];
        ntpData[0] = 0x1B;

        var addresses = Dns.GetHostEntry(ntpServer).AddressList;
        var ipEndPoint = new IPEndPoint(addresses[0], 123);
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        socket.Connect(ipEndPoint);
        socket.Send(ntpData);
        socket.Receive(ntpData);
        socket.Close();

        long intPart = (long)ntpData[40] << 24 | (long)ntpData[41] << 16 | (long)ntpData[42] << 8 | (long)ntpData[43];
        long fractPart = (long)ntpData[44] << 24 | (long)ntpData[45] << 16 | (long)ntpData[46] << 8 | (long)ntpData[47];

        var milliseconds = ((intPart * 1000) + ((fractPart * 1000) / 0x100000000L));
        var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds(milliseconds);

        var gmtTime = networkDateTime.AddHours(gmtOffset);
        return gmtTime;
    }
}
