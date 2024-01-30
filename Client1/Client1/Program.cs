using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class SimpleTcpClient
{
    public static void Main()
    {
        
        byte[] data = new byte[1024];
        string input, stringData;

        
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Console.WriteLine("Client1 is started");

        try
        {
            server.Connect(ipep);
            Console.WriteLine("Client1 connected to server.");
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
        }


        Console.WriteLine("Map`s size is 36. After number of 36, player goes to start state.");
        Console.WriteLine("There is a purchaseable in odd numbers.Player can buy it but there is no market place to sell it.");

        Thread.Sleep(1000);
        
        int recv = server.Receive(data);
        
        stringData = Encoding.ASCII.GetString(data, 0, recv);
        Console.WriteLine(stringData);

        string msg;
        

        while(true)
        {
            Thread.Sleep(1000);
            recv = server.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);
            if (stringData.Substring(6,1).Equals("H"))
            {

                Console.WriteLine("If you want to buy it,press 1 and enter.");
                msg = Console.ReadLine();
                server.Send(Encoding.ASCII.GetBytes(msg));

            }
            
        }
        

        
        Thread.Sleep(3000);

        
        Console.WriteLine("Disconnecting from server...");
        server.Shutdown(SocketShutdown.Both);
        server.Close();
    }
}