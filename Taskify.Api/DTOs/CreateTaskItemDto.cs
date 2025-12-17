using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.DTOs
{
    public class CreateTaskItemDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public string Priority { get; set; } = "Medium";
    }
}
