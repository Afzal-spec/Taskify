namespace Taskify.Api.DTOs.HabitsDtos
{
    public class UpdateHabitDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Frequency { get; set; }
        public bool IsArchived { get; set; }
    }
}
