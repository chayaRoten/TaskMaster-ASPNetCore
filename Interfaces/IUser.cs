using MyTask.Models;
using System.Collections.Generic;

namespace MyTask.Interface
{
    public interface IUserService
    {
        // List<Task1> GetAll();
        // Task1 GetById(int id);
        // int Add(Task1 Task);
        // bool Delete(int id);
        // bool Update(int x,Task1 pizza);
        List<User> GetAllUsers();
        User GetUserById(int id);
        int AddUser(User user);
        bool UpdateUser(int userId, User user);
        bool DeleteUser(int id);



    }
}

