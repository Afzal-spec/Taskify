namespace Taskify.Api.DTOs.HabitsDtos
{
    public class CreateHabitDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Frequency { get; set; }
    }
}
