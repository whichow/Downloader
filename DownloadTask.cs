
using System;
using System.IO;
using System.Net;
using System.Threading;

public delegate void TaskEventHandler();

public class DownloadTask : ITask
{
    public TaskEventHandler taskStart;
    public TaskEventHandler taskStop;

    private const int bufSize = 65536;
    private volatile bool doDownload;
    private string url;
    private string filename;
    private Thread downloadThread;

    public long FileSize
    {
        get;
        private set;
    }
    public long ReceivedSize
    {
        get;
        private set;
    }
    
    public DownloadTask(string url, string filename)
    {
        this.url = url;
        this.filename = filename;
    }

    public void Start()
    {
        if(File.Exists(filename))
        {
            File.Delete(filename);
        }

        Console.WriteLine("Start Download");
        ReceivedSize = 0;
        FileSize = 0;
        if(downloadThread == null)
        {
            downloadThread = new Thread(DoDownload);
            downloadThread.Start();
            doDownload = true;
            if(taskStart != null) taskStart();
        }
    }

    public void Stop()
    {
        Console.WriteLine(".................Stop");
        if(downloadThread != null && downloadThread.IsAlive)
        {        
            doDownload = false;
            downloadThread.Join();
            downloadThread = null;
            if(taskStop != null) taskStop();
        }
    }

    private void DoDownload()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        FileMode filemode;
        if(ReceivedSize == 0)
        {
            filemode = FileMode.CreateNew;
        }
        else
        {
            filemode = FileMode.Append;
            request.AddRange(ReceivedSize);
        }

        using(FileStream fs = new FileStream(filename, filemode))
        {
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream receiveStream = response.GetResponseStream();
                FileSize = response.ContentLength;

                byte[] buf = new byte[bufSize];
                int count;

                while((count = receiveStream.Read(buf, 0, bufSize)) > 0 && doDownload)
                {
                    Console.WriteLine(ReceivedSize);
                    fs.Write(buf, 0, count);    
                    ReceivedSize += count;
                }
            }
        }
        Console.WriteLine("Download Complete");
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}