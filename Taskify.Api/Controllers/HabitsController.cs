using Microsoft.AspNetCore.Mvc;
using Taskify.Api.DTOs.HabitsDtos;
using Taskify.Api.Services;

[ApiController]
[Route("api/habits")]
public class HabitsController : ControllerBase
{
    private readonly HabitService service;

    public HabitsController(HabitService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHabitDto dto)
    {
        var habit = await service.CreateAsync(dto);
        return Ok(habit);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var habit = await service.GetByIdAsync(id);
        if (habit == null) return NotFound();
        return Ok(habit);
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id)
    {
        var success = await service.ArchiveAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
