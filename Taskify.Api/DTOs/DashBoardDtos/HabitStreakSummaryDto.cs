namespace Taskify.Api.DTOs.DashBoardDtos
{
    public class HabitStreakSummaryDto
    {
        public int TotalHabits { get; set; }
        public int CompletedToday { get; set; }
        public BestStreakDto? BestStreak { get; set; }
        public List<ActiveStreakDto> ActiveStreaks { get; set; } = new();
    }

    public class BestStreakDto
    {
        public Guid HabitId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Streak { get; set; }
    }

    public class ActiveStreakDto
    {
        public Guid HabitId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CurrentStreak { get; set; }
    }
}
