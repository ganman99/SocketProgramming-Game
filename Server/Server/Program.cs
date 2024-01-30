using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;
using System.Runtime.CompilerServices;

class SimpleTcpSrvr
{
    
    public static void Main()
    {

        
        Random random = new Random();
        byte[] datalandın = new byte[1024];
        byte[] receiveData = new byte[1024];
        IPEndPoint client1IP = new IPEndPoint(IPAddress.Any, 11111);
        IPEndPoint client222IP = new IPEndPoint(IPAddress.Any, 11112);
        Socket forClient1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket forClient2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int[] board = new int[36];
        int[] homeBelongs = new int[36];


        
        Console.WriteLine("Server is opened.");

        
        forClient1.Bind(client1IP);
        forClient2.Bind(client222IP);

        
        forClient1.Listen(10);
        forClient2.Listen(10);

        
        Console.WriteLine("Waiting for Player1 ...");
        Socket client1 = forClient1.Accept();
        Console.WriteLine("Player1 is joined.");
        Console.WriteLine("Waiting for Player2...");
        Socket client2 = forClient2.Accept();
        Console.WriteLine("Player2 is joined.");

        


        
        int whoFirst = random.Next(1,3);
        string welcome = "Welcome to Monopoly Game.Player"+whoFirst+" is first. "+whoFirst;
        datalandın = Encoding.ASCII.GetBytes(welcome);
        client1.Send(datalandın, datalandın.Length, SocketFlags.None);
        client2.Send(datalandın, datalandın.Length, SocketFlags.None);

        int dice1;
        int dice2;
        int XlocationOfPlayer1 = 0;
        int XlocationOfPlayer2 = 0;
        int XlocationOfPlayer1TEMP = 0;
        int XlocationOfPlayer2TEMP = 0;


        string message = "1 2 2 H "+XlocationOfPlayer1+" "+XlocationOfPlayer2;

        board[XlocationOfPlayer1] = 3;
        board[XlocationOfPlayer2] = 3;

        while (true) {
            Thread.Sleep(3000);
            for (int i = 0; i < homeBelongs.Length; i++)
            {
                Console.Write(homeBelongs[i]+" ");
            }
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.WriteLine("Turn :"+whoFirst);
            dice1 = random.Next(1,7);
            dice2 = random.Next(1,7);

            if (whoFirst == 1)
            {
                if(newLocation(XlocationOfPlayer1,dice2,dice1) % 2 == 1 && homeBelongs[newLocation(XlocationOfPlayer1, dice2, dice1)] == 0 )
                {

                    
                    XlocationOfPlayer1TEMP = newLocation(XlocationOfPlayer1, dice2, dice1);
                    XlocationOfPlayer1 = XlocationOfPlayer1TEMP;
                    message = "1 " + dice1 + " " + dice2 + " H " + XlocationOfPlayer1 + " " + XlocationOfPlayer2;

                    datalandın = Encoding.ASCII.GetBytes(message);
                    client1.Send(datalandın, datalandın.Length, SocketFlags.None);

                    client1.Receive(receiveData);
                    message = Encoding.ASCII.GetString(receiveData);
                    Console.WriteLine(message);
                    if (message.Substring(0,1).Equals("1"))
                    {
                        Console.WriteLine("Hereeeee");
                        homeBelongs[XlocationOfPlayer1] = 1;
                    }
                }
                else
                {
                    
                    XlocationOfPlayer1TEMP = newLocation(XlocationOfPlayer1, dice2, dice1);
                    XlocationOfPlayer1 = XlocationOfPlayer1TEMP;
                    message = "1 " + dice1 + " " + dice2 + " Z " + XlocationOfPlayer1 + " " + XlocationOfPlayer2;

                    datalandın = Encoding.ASCII.GetBytes(message);
                    client1.Send(datalandın, datalandın.Length, SocketFlags.None);


                }
                

                whoFirst++;
                
            }else if (whoFirst == 2)
            {
                if(newLocation(XlocationOfPlayer2, dice2, dice1) % 2 == 1 && homeBelongs[newLocation(XlocationOfPlayer2, dice2, dice1)] == 0)
                {
                    
                    XlocationOfPlayer2TEMP = newLocation(XlocationOfPlayer2, dice2, dice1);
                    XlocationOfPlayer2 = XlocationOfPlayer2TEMP;
                    message = "2 " + dice1 + " " + dice2 + " H " + XlocationOfPlayer1 + " " + XlocationOfPlayer2;

                    datalandın = Encoding.ASCII.GetBytes(message);
                    client2.Send(datalandın, datalandın.Length, SocketFlags.None);

                    client2.Receive(receiveData);
                    message = Encoding.ASCII.GetString(receiveData);
                    if (message.Substring(0, 1).Equals("1"))
                    {
                        homeBelongs[XlocationOfPlayer2] = 2;
                    }
                }
                else
                {
                    
                    XlocationOfPlayer2TEMP = newLocation(XlocationOfPlayer2, dice2, dice1);
                    XlocationOfPlayer2 = XlocationOfPlayer2TEMP;
                    message = "2 " + dice1 + " " + dice2 + " Z " + XlocationOfPlayer1 + " " + XlocationOfPlayer2;

                    datalandın = Encoding.ASCII.GetBytes(message);
                    client2.Send(datalandın, datalandın.Length, SocketFlags.None);
                }
                
                whoFirst--;
            }
            else
            {
                break;
            }
            

        }

        
        
    }
    public void printBoard(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine(arr[i]);
        }
    }
    public static int newLocation(int x , int dice1 , int dice2)
    {
        int xabc = x;
        
        for(int i = 0;i<dice2+dice1;i++)
        {
            if (xabc == 35)
            {
                xabc = 0;
            }
            xabc++;
        }
        return xabc;
    }
    
}

