namespace Taskify.Api.DTOs.DashBoardDtos
{
    public class TodayHabitStatusDto
    {
        public Guid HabitId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public bool IsCompletedToday { get; set; }
    }
}
