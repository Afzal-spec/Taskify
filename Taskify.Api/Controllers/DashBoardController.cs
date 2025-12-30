using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardController : ControllerBase
    {
        private readonly DashBoardService dashBoardService;

        public DashBoardController(DashBoardService dashBoardService)
        {
            this.dashBoardService = dashBoardService;
        }
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayStatus()
        {
            return Ok(await dashBoardService.GetTodayStatusAsync());
        }

        [HttpGet("habits/streak-summary")]
        public async Task<IActionResult> GetHabitStreakSummary()
        {
            return Ok(await dashBoardService.GetHabitStreakSummaryAsync());
        }

        [HttpGet("tasks/summary")]
        public async Task<IActionResult> GetTaskSummary()
        {
            return Ok(await dashBoardService.GetTaskSummaryAsync());
        }

        [HttpGet("today-tasks")]
        public async Task<IActionResult> GetTodayTasks()
        {
            return Ok(await dashBoardService.GetTodayTasksAsync());
        }




    }
}
