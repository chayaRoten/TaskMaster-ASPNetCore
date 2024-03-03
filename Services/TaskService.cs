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
        // private List<User> users;

        private string taskFile = "Task.json";
        // private string userFile = "Users.json";
        public TaskService()
        {

            this.taskFile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Task.json");
            // this.userFile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Users.json");

            using (var jsonFile = File.OpenText(taskFile))
            {
                tasks = JsonSerializer.Deserialize<List<Task1>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            // using (var jsonFile = File.OpenText(userFile))
            // {
            //     users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            //     new JsonSerializerOptions
            //     {
            //         PropertyNameCaseInsensitive = true
            //     });
            // }
        }

        private void saveToFile()
        {
            File.WriteAllText(taskFile, JsonSerializer.Serialize(tasks));
        }

        // private void saveToFileUsers()
        // {
        //     File.WriteAllText(userFile, JsonSerializer.Serialize(users));
        // }
        
        public List<Task1> GetAllTasks(int userId) => tasks.Where(x => x.userId == userId).ToList();

        public Task1 GetTaskById(int id, int userId)
        {
            return tasks.FirstOrDefault(p => p.Id == id && p.userId==userId);
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




        // public List<User> AdminGetAll() => users;

        // public User AdminGetById(string Password)
        // {
        //     return users.FirstOrDefault(p => p.Password == Password);
        // }


        // public string addUser(User user)
        // {
        //     if (users.Count == 0)

        //     {
        //         user.Password = "1";
        //     }
        //     else
        //     {
        //         user.Password = users.Max(p => p.Password) + 1;

        //     }
        //     users.Add(user);
        //     saveToFileUsers();
        //     return user.Password;
        // }


        // public bool updateUser(string password, User user)
        // {
        //     if (password != user.Password)
        //         return false;

        //     var existingUser = AdminGetById(password);
        //     if (existingUser == null)
        //         return false;

        //     var index = users.IndexOf(existingUser);
        //     if (index == -1)
        //         return false;

        //     users[index] = user;
        //     saveToFileUsers();
        //     return true;
        // }

        // public bool deleteUser(string password)
        // {
        //     var existingUser = AdminGetById(password);
        //     if (existingUser == null)
        //         return false;

        //     var index = users.IndexOf(existingUser);
        //     if (index == -1)
        //         return false;

        //     users.RemoveAt(index);
        //     saveToFileUsers();
        //     return true;
        // }

    }

    public static class TaskUtils
    {
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();
        }
    }
}