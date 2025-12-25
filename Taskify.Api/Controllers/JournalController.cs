using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskify.Api.DTOs.JournalDtos;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JournalController : ControllerBase
    {
        private readonly JournalService service;

        public JournalController(JournalService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(CreateJournalEntryDto dto)
        {
            var result = await service.UpsertAsync(dto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entries = await service.GetAllAsync();
            return Ok(entries);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DateTime date)
        {
            var success = await service.DeleteAsync(date);
            if (!success) return NotFound();
            return NoContent();
        }
        // GET deleted journal entries (Recycle Bin)
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var entries = await service.GetDeletedAsync();
            return Ok(entries);
        }

        // RESTORE deleted journal entry
        [HttpPatch("restore")]
        public async Task<IActionResult> Restore([FromQuery] DateTime date)
        {
            var success = await service.RestoreAsync(date);
            if (!success) return NotFound();

            return NoContent();
        }

    }
}
