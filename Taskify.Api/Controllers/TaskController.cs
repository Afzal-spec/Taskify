using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.Api.Data;
using Taskify.Api.Models;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public TaskController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get all task
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetAllTasks()
        {
            var tasks = await dbContext.Tasks.ToListAsync();
            if(tasks == null) return NotFound();
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTaskItem(TaskItem taskItem)
        {
            dbContext.Tasks.Add(taskItem);
            await dbContext.SaveChangesAsync();
            return Ok(taskItem);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetItembyId(int id)
        {
            var task = await dbContext.Tasks.SingleOrDefaultAsync(x => x.Id == id);
            if(task == null) return NotFound();
            return Ok(task);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id) return BadRequest();
            dbContext.Entry(taskItem).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }


    }
}
