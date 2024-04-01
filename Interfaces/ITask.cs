using MyTask.Models;
using System.Collections.Generic;

namespace MyTask.Interface
{
    public interface ITaskService
    {
        List<Task1> GetAllTasks(int userId);
        Task1 GetTaskById(int id, int userId);
        int AddNewTask(Task1 Task, int userId);
        bool DeleteTask(int id, int userId);
        bool UpdateTask(int x, Task1 Task, int userId);
    }
}

