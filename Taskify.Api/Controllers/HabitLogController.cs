using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HabitLogController : ControllerBase
    {
        private readonly HabitLogService service;

        public HabitLogController(HabitLogService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> MarkCompleted(
            [FromBody] MarkHabitCompletedDto dto)
        {
            var result = await service.MarkCompletedAsync(dto);
            return Ok(result);
        }

        // ✅ Get logs for a habit
        [HttpGet]
        public async Task<IActionResult> GetByHabit(Guid habitId)
        {
            var logs = await service.GetByHabitAsync(habitId);
            return Ok(logs);
        }
        [HttpGet("daily-status")]
        public async Task<IActionResult> GetDailyStatus([FromQuery] DateTime? date)
        {
            var targetDate = date ?? DateTime.UtcNow.Date;
            var result = await service.GetDailyStatusAsync(targetDate);
            return Ok(result);
        }
        [HttpGet("{habitId}/streak")]
        public async Task<IActionResult> GetCurrentStreak(Guid habitId)
        {
            var result = await service.GetStreakAsync(habitId);
            return Ok(result);
        }



    }
}
