namespace Taskify.Api.DTOs.DashBoardDtos
{
    public class TaskSummaryDto
    {
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Overdue { get; set; }
        public double CompletionRate { get; set; }
    }
}
