using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    public TcpListener serverSocket;
    public List<TcpClient> clients = new List<TcpClient>();
    public static int num;
    public Server()
    {
        serverSocket = new TcpListener(IPAddress.Any, 9000);
        serverSocket.Start();

        Console.WriteLine("Serverul este pornit");

        // Pornirea unui fir de executie pentru conectarea la clienti in paralel
        Thread acceptClientsThread = new Thread(new ThreadStart(AcceptClients));
        acceptClientsThread.Start();
    }

    public void AcceptClients()
    {
        while (true)
        {
            TcpClient client = serverSocket.AcceptTcpClient();
            clients.Add(client);

            Console.WriteLine("\n#########################################################################################\n");
            Console.WriteLine($"Client conectat: {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");

            // Pornirea unui fir de execuție pentru a gestiona fiecare client în paralel
            Thread clientThread = new Thread(() => DataClient(client));
            clientThread.Start();
        }
    }

    public static void DataClient(TcpClient client)
    {
        string welcomeMessage = "Te-ai conectat la server";
        byte[] welcomeMessageBytes = System.Text.Encoding.ASCII.GetBytes(welcomeMessage);
        Console.ForegroundColor = ConsoleColor.Yellow;
        using (var stream = client.GetStream())
        {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) >= 0)
                {
                    string receivedData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    var clientend = (IPEndPoint)client.Client.RemoteEndPoint;
                    if (string.IsNullOrEmpty(receivedData))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Mesaj de la client : {clientend.Address} :{clientend.Port} \n {receivedData} ", Console.ForegroundColor);

                    }
                    byte[] responseBytes = Encoding.ASCII.GetBytes($"Serveru a primit mesajul : {clientend.Port}");
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
            }
        }
    }

    public static void Start_Program()
    {
        if (num != 0 && num > 0) 
        {
            Server server = new Server();
            for (int i = 0; i < num; i++)
            {
                string DFPath = "D:\\UTM\\ANUL 3\\SEM 2\\PR\\Programarea-in-Retea\\Lab1\\client\\bin\\Debug\\net6.0\\Client.exe";
                ProcessStartInfo info = new ProcessStartInfo(DFPath);
                info.CreateNoWindow = false;
                info.UseShellExecute = true;
                Process processChild = Process.Start(info);
            }
        }
        else
        {
            Console.WriteLine("Date introduse gresit");
            
            MainMenu();
        }
    }

    public static bool MainMenu()
    {
        Console.Clear();
        Console.WriteLine("1) Alege numarul de clienti");
        Console.WriteLine("2) Porneste serverul");
        Console.Write("\rAlege o optiune: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Introdu numarul de clienti: ");
                num = int.Parse(Console.ReadLine());
                MainMenu();
                return true;
            case "2":
                Start_Program();
                return true;
            case "3":
                return false;
            default:
                return true;
        }
    }

    public static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        MainMenu();
    }
}
