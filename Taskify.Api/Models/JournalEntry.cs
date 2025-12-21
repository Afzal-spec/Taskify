using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.Models
{
    public class JournalEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MinLength(10)]
        public string Content { get; set; }
        public int Mood { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
