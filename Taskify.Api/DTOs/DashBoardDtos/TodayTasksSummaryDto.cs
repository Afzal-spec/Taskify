namespace Taskify.Api.DTOs.DashBoardDtos
{
    public class TodayTasksSummaryDto
    {
        public int TotalTasksToday { get; set; }
        public int CompletedToday { get; set; }
        public int PendingToday { get; set; }
        public int OverdueToday { get; set; }
        public List<TodayTaskItemDto> Tasks { get; set; } = new();
    }
}
