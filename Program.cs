using System;
using System.Threading;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadTask task = new DownloadTask("http://7dx.pc6.com/xjq5/image2pdf41501.zip", @"D:\image2pdf41501.zip");
            task.Start();
            while(true)
            {
                if(Console.ReadKey().Key == ConsoleKey.S)
                {
                    task.Stop();
                }
                if(Console.ReadKey().Key == ConsoleKey.R)
                {
                    task.Start();
                }
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
