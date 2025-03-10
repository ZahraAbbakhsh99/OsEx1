using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;


namespace AbbakhshOs4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> links = new List<string>
        {

                "https://www.jowhareh.com/images/Jowhareh/galleries_3/poster_ff070124-8521-4237-89be-c3655bd2c902.jpeg", // 1
                "https://i1.delgarm.com/i/828/9911/26/6028c916d9364.jpg" , //2
                "https://picmap.ir/storage/photos/2022/6/2Mj2oJHBSXgvFdt_640.jpg" , //3
        };

            DownloadManager manager = new DownloadManager();
            manager.StartDownloads(links);

            // Keep the program running until downloads are complete
            Console.ReadLine();
        }
    }
    public class DownloadManager
    {
        public void StartDownloads(List<string> links)
        {
            foreach (string link in links)
            {
                Thread thread = new Thread(() => DownloadFile(link));
                thread.Start();
            }
        }

        private void DownloadFile(string link)
        {
            using (WebClient client = new WebClient())
            {
                /*
                // Modify the file name as needed
                string fileName = link.Substring(link.LastIndexOf("/") + 1);

                // Specify the path where you want to save the downloaded file
                string path = "Downloads/" + fileName;

                client.DownloadFile(link, path);
                */
                string fileName = link.Substring(link.LastIndexOf('/') + 1);
                client.DownloadFile(link, fileName);

                Console.WriteLine($"Downloaded {fileName}");
            }
        }
    }

}

