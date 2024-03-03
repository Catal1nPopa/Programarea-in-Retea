
using Server;
using System;
using System.Diagnostics;

class StartProgram
{
    public static int num = 0;
    static void Main(string[] arg)
    {
        MainMenu();
    }
    public static bool MainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Clear();
        Console.WriteLine("1) Alege numarul de clienti");
        Console.WriteLine("2) Porneste serverul");
        Console.Write("\rAlege o optiune: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Introdu numarul de clienti: ");
                try
                {
                    num = int.Parse(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                MainMenu();
                return true;
            case "2":
                if(num != 0 && num > 0)
                    {
                     Start_Program();
                    }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Nu ai introdus numarul de clienti. \n 1 - afiseaza meniu \n 0 - exit");
                    int s = int.Parse(Console.ReadLine());
                    if( s == 1)
                    {
                        MainMenu();
                    }
                    else
                    {
                        //break;
                        return false;
                    }
                }
                return true;
            default:
                MainMenu();
                return true;
        }
    }
    public static void Start_Program()
    {
        if (num != 0 && num > 0)
        {
            ServerS server = new ServerS();
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
}
