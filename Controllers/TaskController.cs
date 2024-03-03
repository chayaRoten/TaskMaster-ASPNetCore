using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Service;
using MyTask.Interface;
using System.Web;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace MyTask.Controllers;

[ApiController]
[Route("[controller]")]

public class todoController : ControllerBase
// public class TaskController : ControllerBase
{
    private int userId;
    ITaskService TaskService;
    // public TaskController(ITaskService TaskService)
    public todoController(ITaskService TaskService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService = TaskService;
        this.userId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);

    }
    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<List<Task1>> Get()
    {
        return TaskService.GetAllTasks(userId);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<Task1> Get(int id)
    {
        var task = TaskService.GetTaskById(id, userId);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult Post(Task1 newTask)
    {
        newTask.userId = userId;
        var newId = TaskService.AddNewTask(newTask, userId);

        return CreatedAtAction("Post",
            new { id = newId }, TaskService.GetTaskById(newId, userId));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, Task1 newTask)
    {
        newTask.userId = userId;
        var result = TaskService.UpdateTask(id, newTask, userId);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Delete(int id)
    {
        var result = TaskService.DeleteTask(id, userId);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
