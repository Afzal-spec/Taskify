using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskify.Api.DTOs.NotesDtos;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly NoteService service;

        public NotesController(NoteService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await service.GetByIdAsync(id);
            return note == null? NotFound(): Ok(note);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteDto dto)
        {
            var note = await service.CreateAsync(dto);
            return Ok(note);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, CreateNoteDto dto)
        {
            var success = await service.UpdateAsync(id, dto);
            return Ok(success);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var success = await service.DeleteAsync(id);
            return success ? Ok(): NotFound();
        }
    }
}
