using MyTask.Service;
using MyTask.Middlewares;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyTask.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddAuthentication(options =>
     {
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(cfg =>
     {
         cfg.RequireHttpsMetadata = false;
         cfg.TokenValidationParameters = TaskTokenService.GetTokenValidationParameters();
     });
//
builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                // cfg.AddPolicy("Agent", policy => policy.RequireClaim("type", "User"));
                cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
                // cfg.AddPolicy("User", policy => policy.RequireClaim("userId", ""));
                cfg.AddPolicy("ClearanceLevel1", policy => policy.RequireClaim("ClearanceLevel", "1", "2"));
                cfg.AddPolicy("ClearanceLevel2", policy => policy.RequireClaim("ClearanceLevel", "2"));
            });

//
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI", Version = "v1" });
       //auth3
       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Description = "Please enter JWT with Bearer into field",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey
       });
       //auth4
       c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {}
                }
       });
   });







builder.Services.AddControllers();
builder.Services.AddTask();
builder.Services.AddUser();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

var app = builder.Build();

app.UselogMiddleware("file.log");


// app.UseloginMiddleware();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

//auth5
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();

// var er.Build();
// app.UselogMiddleware("file.log");

// // Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseDefaultFiles();
// app.UseStaticFiles();

// //
// app.UseAuthentication();

// app.UseAuthorization();

// app.MapControllers();



// app.Run();



// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Hosting;
// using startup;
// using MyTask.Service;
// using MyTask.Middlewares;

// namespace program
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             // var builder = WebApplication.CreateBuilder(args);
//             // builder.Services.AddControllers();
//             // builder.Services.AddTask();
//             // builder.Services.AddEndpointsApiExplorer();
//             // builder.Services.AddSwaggerGen();
//             // builder.Build().Run();
//             // var builder=CreateHostBuilder(args).Services.AddControllers();
//             // builder.Services.AddControllers();
//             // builder.Services.AddTask();
//             // builder.Services.AddEndpointsApiExplorer();
//             // builder.Services.AddSwaggerGen();
//             // builder.Build().Run();
//             // IHostBuilder b=CreateHostBuilder(args);
//             // b.Services.AddTask();
//             // b.Build().Run();
//             CreateHostBuilder(args).Build().Run();


//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 });
//     }
// }
