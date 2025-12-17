using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.DTOs
{
    public class UpdateTaskItemDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? DueDate { get; set; }
        [Required]
        public string Priority { get; set; } = "Medium";
    }
}
