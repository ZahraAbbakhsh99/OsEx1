using System;
using System.Threading;


namespace AbbakhshOs6.semaphore
{
    internal class Program
    {
        private static Semaphore sem = new Semaphore(3,3);
        static void Main()
        {
            for (int i = 0; i < 7; i++)
            {
                Thread thread = new Thread(DoWork);
                thread.Start(i + 1);
            }
            Console.ReadLine();
        }

        static void DoWork(object obj)
        {
            sem.WaitOne();
            Console.WriteLine("Thread " + obj.ToString());
            Thread.Sleep(1500);
            sem.Release();
        }
    }
}

