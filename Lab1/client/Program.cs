using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        IPAddress serverIp = IPAddress.Parse("127.0.0.1");
        int serverPort = 9000;

        Thread.Sleep(2000);

        while (true)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    client.Connect(serverIp, serverPort);

                    Console.WriteLine("Introdu textul:\n", Console.ForegroundColor);
                    string text = Console.ReadLine() ?? "";
                    Console.WriteLine("\n---------------------\n", Console.ForegroundColor);

                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] bytedata = Encoding.UTF8.GetBytes(text);
                        stream.Write(bytedata, 0, bytedata.Length);

                        byte[] receivedBuffer = new byte[1024];
                        int bytesRead = stream.Read(receivedBuffer, 0, receivedBuffer.Length);
                        string receivedText = Encoding.UTF8.GetString(receivedBuffer, 0, bytesRead);

                        Console.WriteLine($"Server: {receivedText}\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare: {ex.Message}");
                }
            }

            // Thread.Sleep(2000);
        }
    }
}
