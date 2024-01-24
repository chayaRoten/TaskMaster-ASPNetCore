using MyTask.Models;
using MyTask.Interface;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace MyTask.Service{


public class TaskService: ITaskService
{
    // private List<Task1> tasks;

    // public TaskService()
    // {
    //     tasks = new List<Task1>{
    //         new Task1 {Id=1 , Name="aaa" , IsDo=true},
    //         new Task1 {Id=2 , Name="bbb" , IsDo=false},
    //         new Task1 {Id=3 , Name="ccc" , IsDo=true}
    //     };
    // }

        private List<Task1> tasks;
        private string fileName = "Task.json";
        public TaskService()
        {
            this.fileName = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Task.json");

            using (var jsonFile = File.OpenText(fileName))
            {
                tasks = JsonSerializer.Deserialize<List<Task1>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
        }

    public List<Task1> GetAll() => tasks;

    public Task1 GetById(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

    public int Add(Task1 newTask)
    {
        if (tasks.Count == 0)

            {
                newTask.Id = 1;
            }
            else
            {
        newTask.Id =  tasks.Max(p => p.Id) + 1;

            }

        tasks.Add(newTask);
        saveToFile();
        return newTask.Id;
    }
  
    public bool Update(int id, Task1 newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks[index] = newTask;
        saveToFile();
        return true;
    }  

      
    public bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks.RemoveAt(index);
        saveToFile();
        return true;
    }  

    

}

public static class TaskUtils
    {
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();
        }
    }
}