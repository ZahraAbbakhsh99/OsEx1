using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbbakhshOs6.monitor
{
    internal class Program
    {
        private static object qw = new object();
        static void Main(string[] args)
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
            Monitor.Enter(qw);
            Console.WriteLine("Thread " + obj.ToString());
            Thread.Sleep(1000);
            Monitor.Exit(qw);
          
        }

    }
}
