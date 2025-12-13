using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Taskify.Api.Data;
using Taskify.Api.DTOs;
using Taskify.Api.Models;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class TaskController : ControllerBase
    //{
    //    private readonly AppDbContext dbContext;

    //    public TaskController(AppDbContext dbContext)
    //    {
    //        this.dbContext = dbContext;
    //    }

    //    //get all task
    //    [HttpGet]
    //    public async Task<ActionResult<List<TaskItem>>> GetAllTasks()
    //    {
    //        var tasks = await dbContext.Tasks.ToListAsync();
    //        if(tasks == null) return NotFound();
    //        return Ok(tasks);
    //    }
    //    [HttpPost]
    //    public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
    //    {
    //        dbContext.Tasks.Add(taskItem);
    //        await dbContext.SaveChangesAsync();
    //        return Ok(taskItem);
    //    }
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<TaskItem>> GetTaskbyId(int id)
    //    {
    //        var task = await dbContext.Tasks.SingleOrDefaultAsync(x => x.Id == id);
    //        if(task == null) return NotFound();
    //        return Ok(task);
    //    }
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
    //    {
    //        if (id != taskItem.Id) return BadRequest();
    //        dbContext.Entry(taskItem).State = EntityState.Modified;
    //        await dbContext.SaveChangesAsync();

    //        return NoContent();
    //    }
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteTask(int id)
    //    {
    //        var taskItem = await dbContext.Tasks.FindAsync(id);
    //        if(taskItem == null) return NotFound();
    //        dbContext.Tasks.Remove(taskItem);
    //        await dbContext.SaveChangesAsync();
    //        return NoContent();
    //    }


    //}

    public class TaskController : ControllerBase
    {
        private readonly TaskService service;

        public TaskController(TaskService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskItemDto dto)
        {
            var created = await service.CreateAsync(dto);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskItemDto dto)
        {
            var success = await service.UpdateAsync(id, dto);
            if(!success) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await service.DeleteAsync(id);
            if(!success) return NotFound();
            return NoContent();
        }

    }

}
