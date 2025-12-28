namespace Taskify.Api.DTOs.HabitsDtos
{
    public class DailyHabitStatusDto
    {
        public Guid HabitId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
    }
}
