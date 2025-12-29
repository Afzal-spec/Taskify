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


    }
}
