using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerS
    {
        private Socket socket;
        private List<Socket> connClients;

        public ServerS()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connClients = new List<Socket>();
            Start();
        }

        public void Start()
        {
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000));
            socket.Listen(10);

            Thread listenerThread = new Thread(Listener);
            listenerThread.Start();
        }

        public void Listener()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Asteptare conexiune...");

                    Socket clientSocket = socket.Accept();
                    connClients.Add(clientSocket);
                    
                    var clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                    string clientInfo = $"Client conectat: IP: {clientEndPoint.Address}, Port: {clientEndPoint.Port}";
                    Console.WriteLine(clientInfo); 

                    Thread clientThread = new Thread(() => ClientData(clientSocket));
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                socket.Close();
            }
        }

        public void ClientData(Socket socketClient)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while((bytesRead = socketClient.Receive(buffer)) > 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0 , bytesRead);
                    if (string.IsNullOrEmpty(dataReceived))
                    {
                        Console.WriteLine("Mesaj gol!");
                        continue;
                    }
                    broadcast($"Client {socketClient.RemoteEndPoint}: {dataReceived}", socketClient);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void broadcast(string message, Socket socketSender)
        {
                foreach(var clientSocket in connClients)
                {
                    if (clientSocket != socketSender)
                    {
                        try
                        {
                            byte[] buffer = Encoding.ASCII.GetBytes(message);
                            clientSocket.Send(buffer);
                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine($"Eroare la trimitere broadcast catre: {clientSocket.RemoteEndPoint}: {ex.Message}");
                        }
                    }
                }
            
        }
    }
}
