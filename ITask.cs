using System;

public interface ITask : IDisposable
{
    void Start();
    void Stop();
}