using Task.Models;
using System.Collections.Generic;

namespace Task.Interface
{
    public interface ITaskService
    {
        List<Task1> GetAll();
        Task1 GetById(int id);
        int Add(Task1 Task);
        bool Delete(int id);
        bool Update(int x,Task1 pizza);
        // int Count {get;}
    }
}

