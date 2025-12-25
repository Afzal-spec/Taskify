using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.Models
{
    public class HabitLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; } = true;

        // 🔗 Habit
        public Guid HabitId { get; set; }
        public Habit Habit { get; set; }

    }
}
