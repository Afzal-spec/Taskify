using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.Models
{
    public class TaskItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? DueDate { get; set; }
        [Required]
        public string Priority { get; set; } = "Medium";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
