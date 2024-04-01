using MyTask.Models;
using System.Collections.Generic;

namespace MyTask.Interface
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        int AddUser(User user);
        bool UpdateUser(int userId, User user);
        bool DeleteUser(int id);
    }
}

