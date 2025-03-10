using System;
using System.Threading;

namespace AbbakhshOs6.@lock
{
    internal class Program
    {
        private static object lock1 = new object();
        static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(DoWork);
                thread.Start(i+1);
            }
            Console.ReadLine();
        }

        static void DoWork(object obj)
        {
            lock (lock1)
            {
                Console.WriteLine("Thread " + obj.ToString() , Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
        }
    }
}

