using MyTask.Models;
using MyTask.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;

namespace MyTask.Service
{


    public class TaskService : ITaskService
    {
        private List<Task1> tasks;
        private string taskFile = "Task.json";
        public TaskService()
        {

            this.taskFile = Path.Combine("Data", "Task.json");
            using (var jsonFile = File.OpenText(taskFile))
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
            File.WriteAllText(taskFile, JsonSerializer.Serialize(tasks));
        }
        public List<Task1> GetAllTasks(int userId) => tasks.Where(x => x.userId == userId).ToList();
        public Task1 GetTaskById(int id, int userId)
        {
            return tasks.FirstOrDefault(p => p.Id == id && p.userId == userId);
        }
        public int AddNewTask(Task1 newTask, int userId)
        {
            if (tasks.Count == 0)
            {
                newTask.Id = 1;
            }
            else
            {
                newTask.Id = tasks.Max(p => p.Id) + 1;
            }

            tasks.Add(newTask);
            saveToFile();
            return newTask.Id;
        }
        public bool UpdateTask(int id, Task1 newTask, int userId)
        {
            if (id != newTask.Id)
                return false;

            var existingTask = GetTaskById(id, userId);
            if (existingTask == null)
                return false;

            var index = tasks.IndexOf(existingTask);
            if (index == -1)
                return false;

            tasks[index] = newTask;
            saveToFile();
            return true;
        }
        public bool DeleteTask(int id, int userId)
        {
            var existingTask = GetTaskById(id, userId);
            if (existingTask == null)
                return false;

            var index = tasks.IndexOf(existingTask);
            if (index == -1)
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