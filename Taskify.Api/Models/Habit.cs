using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.Models
{
    public class Habit
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // DAILY for now
        public string Frequency { get; set; } = "Daily";

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 User
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation
        public ICollection<HabitLog> Logs { get; set; }
    }
}
