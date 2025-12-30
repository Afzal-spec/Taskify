namespace Taskify.Api.DTOs.DashBoardDtos
{
    public class TodayTaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Priority { get; set; } = string.Empty;
    }
}
