using System;
using System.Threading;

namespace AbbakhshOs6.mutex

{
    internal class Program
    {
        private static Mutex mu = new Mutex();
        static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(DoWork);
                thread.Start(i + 1);
            }
            Console.ReadLine();
        }

        static void DoWork(object obj)
        {
            mu.WaitOne();
            Console.WriteLine("Thread " + obj.ToString(), Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            mu.ReleaseMutex();
        }

    }
}
