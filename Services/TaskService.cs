using Task.Models;
namespace Task.Service;

public static class TaskService
{
    private static List<Task1> tasks;

    static TaskService()
    {
        tasks = new List<Task1>{
            new Task1 {Id=1 , Name="aaa" , IsDo=true},
            new Task1 {Id=2 , Name="bbb" , IsDo=false},
            new Task1 {Id=3 , Name="ccc" , IsDo=true}
        };
    }

    public static List<Task1> GetAll() => tasks;

    public static Task1 GetById(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

    public static int Add(Task1 newTask)
    {
        if (tasks.Count == 0)

            {
                newTask.Id = 1;
            }
            else
            {
        newTask.Id =  tasks.Max(p => p.Id) + 1;

            }

        tasks.Add(newTask);

        return newTask.Id;
    }
  
    public static bool Update(int id, Task1 newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks[index] = newTask;

        return true;
    }  

      
    public static bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks.RemoveAt(index);
        return true;
    }  

}