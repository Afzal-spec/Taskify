namespace Taskify.Api.DTOs.HabitsDtos
{
    public class HabitStreakDto
    {
        public Guid HabitId { get; set; }
        public string HabitName { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
    }
}
