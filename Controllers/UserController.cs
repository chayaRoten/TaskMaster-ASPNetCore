using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Service;
using MyTask.Interface;
using Microsoft.AspNetCore.Authorization;
namespace MyTask.Controllers;

[ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    IUserService UserService;
    public userController(IUserService UserService)
    {
        this.UserService = UserService;
    }

    // [HttpGet]
    // public ActionResult<List<User>> Get()
    // {
    //     return UserService.AdminGetAll();
    // }

    [Authorize(Policy = "User")]        
    [HttpGet("{password}")]
    public ActionResult<User> Get(int userFile)
    {
        var user = UserService.GetUserById(userFile);
        if (user == null)
            return NotFound();
        return user;
    }

    // [HttpPost]
    // public ActionResult Post(User user)
    // {
    //     var newPassword = UserService.addUser(user);

    //     return CreatedAtAction("Post", 
    //         new {password = newPassword}, UserService.AdminGetById(newPassword));
    // }

    // [HttpPut("{password}")]
    // public ActionResult Put(string password,User user)
    // {
    //     var result = UserService.updateUser(password, user);
    //     if (!result)
    //     {
    //         return BadRequest();
    //     }
    //     return NoContent();
    // }

    // [HttpDelete("{password}")]
    // public ActionResult deleteUser(string password)
    // {
    //     var result = UserService.deleteUser(password);
    //     if (!result)
    //     {
    //         return BadRequest();
    //     }
    //     return NoContent();
    // }
}
