namespace Taskify.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
        public List<TaskItem> Tasks { get; set; } = new();
        public List<Note> Notes { get; set; } = new();
        public ICollection<Habit> Habits { get; set; } = new List<Habit>();

    }
}
