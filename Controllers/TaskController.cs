using Microsoft.AspNetCore.Mvc;
using MyTask.Models;
using MyTask.Service;
using MyTask.Interface;

namespace MyTask.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    ITaskService TaskService;
    public TaskController(ITaskService TaskService)
    {
        this.TaskService=TaskService;
    }
    [HttpGet]
    public ActionResult<List<Task1>> Get()
    {
        return TaskService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Task1> Get(int id)
    {
        var task = TaskService.GetById(id);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    public ActionResult Post(Task1 newTask)
    {
        var newId = TaskService.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskService.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,Task1 newTask)
    {
        var result = TaskService.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
