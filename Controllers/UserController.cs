using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Service;
using MyTask.Interface;
using Microsoft.AspNetCore.Authorization;
namespace MyTask.Controllers;
using System.Linq;
using System.IO;
using System.Text.Json;

[ApiController]
[Route("[controller]")]

public class userController : ControllerBase
{
    private List<User> users;
    private List<User> userlist=new List<User>();
    private string userFile = "Users.json";
    private int userId;
    IUserService UserService;
    public userController(IUserService UserService , IHttpContextAccessor httpContextAccessor)
    {
        this.UserService = UserService;
        this.userId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
        this.userFile = Path.Combine("Data", "Users.json");
            using (var jsonFile = System.IO.File.OpenText(userFile))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
    }


    // public ActionResult<List<User>> Get()
    // {
    //     var currentUser = users.Find(x => x.userId == userId);
    //     if (currentUser.isAdmin == false)
    //     {
    //         var user = UserService.GetUserById(userId);
    //         if (user == null)
    //             return NotFound();
    //         userlist.Add(user);
    //         return userlist;
    //     }
    //     else
    //     {
    //         return UserService.GetAllUsers();
    //     }
    // }

    [Authorize(Policy = "Admin")]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<List<User>> GetAll()
    {
        return UserService.GetAllUsers();
    }


    [Authorize(Policy = "User")]
    [HttpGet]
    public ActionResult<User> Get()
    {
        var user = UserService.GetUserById(userId);
        if (user == null)
            return NotFound();
        userlist.Add(user);
        return user;
    }

    [Authorize(Policy = "User")]        
    [HttpPut]
    public ActionResult Put(User user)
    {
        var result = UserService.UpdateUser(userId, user);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User user)
    {
        var newUserId = UserService.AddUser(user);

        return CreatedAtAction("Post", 
            new {userId = newUserId}, UserService.GetUserById(newUserId));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult deleteUser(int id)
    {
        var result = UserService.DeleteUser(id);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
