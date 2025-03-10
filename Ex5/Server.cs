using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Http;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleApp5
{
    class Program
    {
        static TcpListener tcpServer;
        static TcpClient tcpClient;
        static Thread th;
        static string input;
        public static void Main()
        {
            th = new Thread(new ThreadStart(StartListen));
            th.Start();
        }
        public static void StartListen()
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                tcpServer = new TcpListener(localAddr, 5000);
                tcpServer.Start();

                while (true)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(NewClient));
                    tcpClient = tcpServer.AcceptTcpClient();
                    t.Start(tcpClient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void NewClient(Object obj)
        {
            Console.WriteLine("Client Added");
            var client = (TcpClient)obj;
            NetworkStream ns = client.GetStream();

            StateObject state = new StateObject();
            state.workSocket = client.Client;
            client.Client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback((new Program()).OnReceive), state);

            while (true)
            {
                // Read file content and send it over the network stream
                string filePath = "file.txt"; // Change this to the path of the file you want to transfer
                byte[] fileBytes = File.ReadAllBytes(filePath);
                ns.Write(fileBytes, 0, fileBytes.Length);
                ns.Flush();
                break; // Break after sending the file once
            }
            Console.ReadKey();
        }
        public void OnReceive(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead;

            if (handler.Connected)
            {
                try
                {
                    bytesRead = handler.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        // Save received file content to a file
                        string receivedFilePath = "received_file.txt";
                        using (FileStream fs = new FileStream(receivedFilePath, FileMode.Append))
                        {
                            fs.Write(state.buffer, 0, bytesRead);
                        }

                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(OnReceive), state);
                    }
                }
                catch (SocketException socketException)
                {
                    if (socketException.ErrorCode == 10054 || ((socketException.ErrorCode != 10004) && (socketException.ErrorCode != 10053)))
                    {
                        handler.Close();
                    }
                }
                catch (Exception exception)
                {
                    // Handle exceptions
                }
            }
        }
    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}