namespace Taskify.Api.DTOs.HabitsDtos
{
    public class HabitLogDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public Guid HabitId { get; set; }
    }
}
