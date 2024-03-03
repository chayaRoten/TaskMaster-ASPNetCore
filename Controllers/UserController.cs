using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Service;
using MyTask.Interface;

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

    [HttpGet("{password}")]
    public ActionResult<User> Get(string password)
    {
        var user = UserService.GetUserById(password);
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
