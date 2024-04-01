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
        private List<User> users;
        private string userFile = "Users.json";
        public UserService()
        {
            this.userFile = Path.Combine("Data", "Users.json");
            using (var jsonFile = File.OpenText(userFile))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFileUsers()
        {
            File.WriteAllText(userFile, JsonSerializer.Serialize(users));
        }

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

            var existingUser = GetUserById(userId);
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