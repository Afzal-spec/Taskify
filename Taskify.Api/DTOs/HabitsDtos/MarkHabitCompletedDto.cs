public class MarkHabitCompletedDto
{
    public Guid HabitId { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }
}