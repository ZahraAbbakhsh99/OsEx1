using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TcpClientSample
{
    public static void Main()
    {
        int port;
        TcpClient server;

        System.Console.WriteLine("Please Enter the port number of Server:\n");
        port = Int32.Parse(System.Console.ReadLine());
        try
        {
            server = new TcpClient("127.0.0.1", port);
        }
        catch (SocketException)
        {
            Console.WriteLine("Unable to connect to server");
            return;
        }
        Console.WriteLine("Connected to the Server...");

        NetworkStream ns = server.GetStream();

        Console.WriteLine("Enter the path to the file you want to send");
        string filePath = Console.ReadLine();
        //string filePath = @"C:Users\TetaSoft\source\repos\AbbakhshOs5.client\AbbakhshOs5.client\bin\Debug";
        byte[] fileData = File.ReadAllBytes(filePath);
        ns.Write(fileData, 0, fileData.Length);
        ns.Flush();

        Console.WriteLine("File sent to the server.");

        Console.WriteLine("Disconnecting from server...");
        ns.Close();
        server.Close();
    }
}
