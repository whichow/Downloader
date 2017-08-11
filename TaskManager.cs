using System.Collections.Generic;

public abstract class TaskManager
{
    private List<ITask> tasks;

    public void AddTask(ITask task)
    {
        tasks.Add(task);
    }
    public void RemoveTask(ITask task)
    {
        tasks.Remove(task);
    }
    public void StartTask(ITask task)
    {
        task.Start();
    }

    public void StopTask(ITask task)
    {
        task.Stop();
    }
}