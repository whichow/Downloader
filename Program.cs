using System;
using System.Threading;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadTask task = new DownloadTask("http://coserarea.com/test/Framework/Framework_Res/framework_model.unity3d", @"D:\framework_model.unity3d");
            task.Start();
            while(true)
            {
                if(Console.ReadKey().Key == ConsoleKey.S)
                {
                    task.Stop();
                }
                if(Console.ReadKey().Key == ConsoleKey.R)
                {
                    task.Resume();
                }
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
