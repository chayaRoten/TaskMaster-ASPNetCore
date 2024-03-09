using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Services;
using MyTask.Interface;
using System.Linq;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;


namespace MyTask.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        IUserService UserService;
        private List<User> users;
        private string userFile = "Users.json";
        public AdminController(IUserService UserService) {
            this.UserService=UserService; 
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
       
        // [HttpGet]
        // [Route("[action]")]
        // public IActionResult GoogleAuthentication()
        // {
        //     // Redirect to the Google authentication provider
        //     return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Google");
        // }

        // [HttpGet]
        // [Route("[action]")]
        // public async Task<IActionResult> GoogleCallback()
        // {
        //     // Handle the Google authentication callback
        //     var result = await HttpContext.AuthenticateAsync("Google");
        //     // Process the authentication result and sign in the user
        //     // Redirect or display appropriate message
        //     return RedirectToAction("Index", "Home");
        // }
    
        [HttpPost]
        [Route("[action]")]
        public ActionResult Login([FromBody] User User ) {
            var user = users.FirstOrDefault(x => x.Username == User.Username && x.Password == User.Password);
            if(user == null)
                return Unauthorized();
                    var claims = new List<Claim>
                    {
                        new Claim("type" ,"User"),
                        new Claim("userId", user.userId.ToString())
                    };
                    if(user.isAdmin==true)
                        claims.Add(new Claim("type", "Admin"));
            var token = TaskTokenService.GetToken(claims);

            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }


        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]

        public ActionResult<List<User>> Get()
        {
            
            return UserService.GetAllUsers();
        }


        [HttpPost]
        [Authorize(Policy="Admin")]
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
}
