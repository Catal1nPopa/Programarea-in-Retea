using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class Server
{
    public TcpListener serverSocket;
    public List<TcpClient> clients = new List<TcpClient>();

    public Server()
    {
        serverSocket = new TcpListener(IPAddress.Any, 9000);
        serverSocket.Start();

        Console.WriteLine("Serverul este pornit");

        // Pornirea unui fir de executie pentru conectarea la clienti in paralel
        Thread acceptClientsThread = new Thread(new ThreadStart(AcceptClients));
        acceptClientsThread.Start();
    }

    private void AcceptClients()
    {
        while (true)
        {
            TcpClient client = serverSocket.AcceptTcpClient();
            clients.Add(client);

            Console.WriteLine($"Client conectat: {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");


            // Pornirea unui fir de execuție pentru a gestiona fiecare client în paralel
            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
            clientThread.Start(client);
        }
    }

    private void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream clientStream = client.GetStream();

        string welcomeMessage = "Te-ai conectat la server";
        byte[] welcomeMessageBytes = System.Text.Encoding.ASCII.GetBytes(welcomeMessage);
        clientStream.Write(welcomeMessageBytes, 0, welcomeMessageBytes.Length);

        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = clientStream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Clientul a transmis toate datele. Client Deconectat \n ---------------------\n");
                    break;
                }

                string receivedData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Mesaj de la client: {receivedData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
                break; 
            }
        }

        client.Close();
        clients.Remove(client);
    }





    public static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Red;

        int num;
        Console.WriteLine("Introdu numarul de clienti: ", Console.ForegroundColor);
        num = int.Parse(Console.ReadLine());

        Server server = new Server();
        for(int i = 0; i < num; i++)
        {
            string DFPath = "D:\\UTM\\ANUL 3\\SEM 2\\PR\\Lab1\\client\\bin\\Debug\\net6.0\\Client.exe";
            //System.Environment.CurrentDirectory = "D:\\UTM\\ANUL 3\\SEM 2\\PR\\Lab1\\client\\bin\\Debug\\net6.0\\Client.exe";
            //Process.Start(@DFPath);
            ProcessStartInfo info = new ProcessStartInfo(DFPath);
            info.CreateNoWindow = false;
            info.UseShellExecute = true;
            Process processChild = Process.Start(info);
        }
    }
}
