using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System;
using System.IO;

namespace AbbakhshOs2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // View connecting and disconnecting USB devices 
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_USBControllerDevice'");
            watcher.EventArrived += (sender, e) =>
            {
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                string deviceId = instance["Dependent"].ToString();

                //Run the mspaint if the USB device is connected.
                Console.WriteLine("A USB drive is connected!");

                //specify the execution path of the mspaint.
                string paintPath = @"C:\Windows\System32\mspaint.exe";

                // Ensuring the existence of mspaint's executable file
                if (File.Exists(paintPath))
                {
                    try
                    {
                        // Openning mspaint
                        Process.Start(paintPath);
                        Console.WriteLine("mspaint opened successfully!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There was an error opening the application : {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("mspaint wasn't found in the specified path");
                }
            };

            watcher.Query = query;
            watcher.Start();

            Console.WriteLine("Waiting for USB device connection");
            Console.ReadLine(); 
            watcher.Stop(); 
        }
    }
}

