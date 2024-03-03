using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

class Program
{
    private static Socket client;

    static void Main()
    {
        try
        {
            IPAddress serverIp = IPAddress.Parse("127.0.0.1");
            int serverPort = 9000;

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(serverIp, serverPort));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Esti conectat la server! \n");

            var clientLocal = (IPEndPoint)client.LocalEndPoint;
            Console.WriteLine($"Esti conectat cu portul:{clientLocal.Port}");

            //Pornim un thread now pentru primirea mesajelor
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            while (true)
            {
                Console.WriteLine("Scrie mesajul:");
                string message = Console.ReadLine();
                byte[] byteData = Encoding.UTF8.GetBytes(message);
                client.Send(byteData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare nu este gasit serverul \n, {ex.Message}");
            client.Close();
        }
    }

    private static bool receivingMessages = true;
    static void ReceiveMessages()
    {
        while (receivingMessages)
        {
            try
            {
                byte[] receivedBuffer = new byte[1024];
                int bytesRead = client.Receive(receivedBuffer);
                string receivedText = Encoding.UTF8.GetString(receivedBuffer, 0, bytesRead);
                Console.WriteLine($"Server: {receivedText}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erroare primire mesaj: {ex.Message}");
                break;
            }
        }
    }
}
