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
    public class loginController : ControllerBase
    {
        IUserService UserService;
        private List<User> users;
        private string userFile;
        public loginController(IUserService UserService)
        {
            this.UserService = UserService;
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

        [HttpPost]
        public ActionResult Login([FromBody] User User)
        {
            var user = users.FirstOrDefault(x => x.Username == User.Username && x.Password == User.Password);
            if (user == null)
                return Unauthorized();
            var claims = new List<Claim>
                    {
                        new Claim("type" ,"User"),
                        new Claim("userId", user.userId.ToString())
                    };
            if (user.isAdmin == true)
                claims.Add(new Claim("type", "Admin"));
            var token = TaskTokenService.GetToken(claims);
            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }
    }
}
