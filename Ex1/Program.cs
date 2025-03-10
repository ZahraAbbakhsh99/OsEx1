using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace AbbakhshOs1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;


            Console.WriteLine("Hi...");
            Boolean Continue = true;
            while (Continue)
            {
                Console.WriteLine("Please choose one of the options below.");
                Console.WriteLine("1" + "\t" + "Start a process.");
                Console.WriteLine("2" + "\t" + "Show The list of processes.");
                Console.WriteLine("3" + "\t" + "Kill a process.");
                Console.WriteLine("4" + "\t" + "Show The Parent of a process.");
                Console.WriteLine("5" + "\t" + "Get process ID by its name.");
                Console.WriteLine("6" + "\t" + "Exit");
                Console.WriteLine("---------------------------------------------");

                int numCH = int.Parse(Console.ReadLine());
               
                switch (numCH)
                {
                    case 1:
                        Console.WriteLine("Please type the name of the program you want to open.");
                        String proNameStart = Console.ReadLine();
                        Choose1(proNameStart);
                        break;
                    case 2:
                        Choose2();
                        break;
                    case 3:
                        Console.WriteLine("Please type the ID of the program you want to kill.");
                        int proIDKill = int.Parse(Console.ReadLine());
                        Choose3(proIDKill);
                        break;
                    case 4:
                        Console.WriteLine("Please type the ID of the program you want to see its Parent.");
                        int proIDParent = int.Parse(Console.ReadLine());
                        Choose4(proIDParent);
                        break;
                    case 5:
                        Console.WriteLine("Please type the Name of the program you want to its ID");
                        String proName =Console.ReadLine();
                        Choose5(proName);
                        break;
                    case 6:
                        Continue = false;
                        Console.WriteLine("bye..");
                        System.Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("The input is invalid." + "\t" + "Try again...");
                        break;
                }
            }
            void Choose1(String proName)
            {
                Boolean ok = true;
                try
                {
                    Process.Start(proName);
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("The process name entered by you is not correct." + "\t" + "Try again...");
                    Console.WriteLine("---------------------------------------------");
                }
                if (ok)
                {
                    Console.WriteLine("Your process started running with the name of " + proName + " .");
                    Console.WriteLine("---------------------------------------------");
                }
            }


            void Choose2()
            {
                foreach (Process pr in Process.GetProcesses())
                {
                    Console.WriteLine(pr.Id + "\t" + pr.ProcessName);
                }
                Console.WriteLine("---------------------------------------------");
            }


            void Choose3(int proID)
            {
                Boolean ok = true;
                try
                {
                    Process localById = Process.GetProcessById(proID);
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Your process ID is not correct." + "\t" + "Try again...");
                }
                if (ok)
                {
                    Process.GetProcessById(proID).Kill();
                    Console.WriteLine("Your process was killed." + "\n" + "This process no longer exists" +
                        " in new list of running processes...");
                    Console.WriteLine("Do you want to new list of Processes ?(y/anything)");
                    string answer = Console.ReadLine();
                    if (answer == "y")
                    Choose2();
                }

                Console.WriteLine("---------------------------------------------");
            }


            void Choose4(int proID)
            {
                try
                {
                    Process pr = Process.GetProcessById(proID);
                    FindParent(pr);
                }
                catch
                {
                    Console.WriteLine("Your process ID is not correct." + "\t" + "Try again...");
                }
               Console.WriteLine("---------------------------------------------");

            }

            void FindParent(Process pr)
            {
                try
                {
                    var performanceCounter = new PerformanceCounter("Process", "Creating Process ID", pr.ProcessName);
                    Process parent = Process.GetProcessById((int)performanceCounter.RawValue);
                    Console.WriteLine("Process {0}(pid {1}) was started by Process {2}(Pid {3})",
                                pr.ProcessName, pr.Id, parent.ProcessName, parent.Id);
                }
                catch
                {
                   Console.WriteLine("The entered process is a system process "+ "\n" +
                      "or its parent has been killed and Now it's guided by the System ");
                }
            }


            void Choose5 (String proName)
            {
                try
                {
                    Process[] pro = Process.GetProcessesByName(proName);
                    Console.WriteLine("ProcessID : " + pro[0].Id);
                }
                catch
                {
                    Console.WriteLine("The Process associated with the entered name wasn't found.");
                }
               Console.WriteLine("---------------------------------------------");
            }
           }
        }    
    }


