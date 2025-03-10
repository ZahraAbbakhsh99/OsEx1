using System;
using System.Threading;


namespace AbbakhshOs6.autoResetEvent
{
    internal class Program
    {
        private static AutoResetEvent are = new AutoResetEvent(true);
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
            are.WaitOne();
            Console.WriteLine("Thread " + obj.ToString(), Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            are.Set();
        }
    }
}
