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
    private int userId;
    IUserService UserService;
    public userController(IUserService UserService , IHttpContextAccessor httpContextAccessor)
    {
        this.UserService = UserService;
        this.userId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
    }

    // [HttpGet]
    // public ActionResult<List<User>> Get()
    // {
    //     return UserService.AdminGetAll();
    // }

    [Authorize(Policy = "User")]        
    [HttpGet]
    public ActionResult<User> Get()
    {
        var user = UserService.GetUserById(userId);
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
    [Authorize(Policy = "User")]        
    [HttpPut("{userId}")]
    public ActionResult Put(int userId,User user)
    {
        var result = UserService.UpdateUser(userId, user);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

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
