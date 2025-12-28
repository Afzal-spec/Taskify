using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.DTOs.HabitsDtos
{
    public class CreateHabitLogDto
    {
        [Required]
        public Guid HabitId { get; set; }

        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; } = true;
    }
}
