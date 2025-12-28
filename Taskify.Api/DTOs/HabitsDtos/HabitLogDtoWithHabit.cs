namespace Taskify.Api.DTOs.HabitsDtos
{
    public class HabitLogDtoWithHabit
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public Guid HabitId { get; set; }
        public string HabitName { get; set; } = string.Empty;
        public string? HabitDescription { get; set; }
        public string Frequency { get; set; } = string.Empty;
    }
}
