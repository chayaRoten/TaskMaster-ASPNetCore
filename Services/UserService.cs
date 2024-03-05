using MyTask.Models;
using MyTask.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;

namespace MyTask.Service
{


    public class UserService : IUserService
    {

        // private List<Task1> tasks;
        private List<User> users;

        // private string taskFile = "Task.json";
        private string userFile = "Users.json";
        public UserService()
        {

            // this.taskFile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Task.json");
            this.userFile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Users.json");

            // using (var jsonFile = File.OpenText(taskFile))
            // {
            //     tasks = JsonSerializer.Deserialize<List<Task1>>(jsonFile.ReadToEnd(),
            //     new JsonSerializerOptions
            //     {
            //         PropertyNameCaseInsensitive = true
            //     });
            // }

            using (var jsonFile = File.OpenText(userFile))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        // private void saveToFile()
        // {
        //     File.WriteAllText(taskFile, JsonSerializer.Serialize(tasks));
        // }

        private void saveToFileUsers()
        {
            File.WriteAllText(userFile, JsonSerializer.Serialize(users));
        }

        // public List<Task1> GetAll() => tasks;

        // public Task1 GetById(int id)
        // {
        //     return tasks.FirstOrDefault(p => p.Id == id);
        // }

        // public int Add(Task1 newTask)
        // {
        //     if (tasks.Count == 0)

        //     {
        //         newTask.Id = 1;
        //     }
        //     else
        //     {
        //         newTask.Id = tasks.Max(p => p.Id) + 1;

        //     }

        //     tasks.Add(newTask);
        //     saveToFile();
        //     return newTask.Id;
        // }

        // public bool Update(int id, Task1 newTask)
        // {
        //     if (id != newTask.Id)
        //         return false;

        //     var existingTask = GetById(id);
        //     if (existingTask == null)
        //         return false;

        //     var index = tasks.IndexOf(existingTask);
        //     if (index == -1)
        //         return false;

        //     tasks[index] = newTask;
        //     saveToFile();
        //     return true;
        // }


        // public bool Delete(int id)
        // {
        //     var existingTask = GetById(id);
        //     if (existingTask == null)
        //         return false;

        //     var index = tasks.IndexOf(existingTask);
        //     if (index == -1)
        //         return false;

        //     tasks.RemoveAt(index);
        //     saveToFile();
        //     return true;
        // }




        public List<User> GetAllUsers() => users;

        public User GetUserById(int userId)
        {
            return users.FirstOrDefault(p => p.userId == userId);
        }


        public int AddUser(User user)
        {
            if (users.Count == 0)

            {
                user.userId = 1;
            }
            else
            {
                user.userId = users.Max(p => p.userId) + 1;

            }
            users.Add(user);
            saveToFileUsers();
            return user.userId;
        }


        public bool UpdateUser(int userId, User user)
        {
            if (userId != user.userId)
                return false;

            var existingUser =GetUserById(userId);
            if (existingUser == null)
                return false;

            var index = users.IndexOf(existingUser);
            if (index == -1)
                return false;

            users[index] = user;
            saveToFileUsers();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var existingUser = GetUserById(userId);
            if (existingUser == null)
                return false;

            var index = users.IndexOf(existingUser);
            if (index == -1)
                return false;

            users.RemoveAt(index);
            saveToFileUsers();
            return true;
        }

    }

    public static class UserUtils
    {
        public static void AddUser(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
        }
    }
}