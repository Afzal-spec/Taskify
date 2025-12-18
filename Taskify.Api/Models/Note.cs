namespace Taskify.Api.Models
{
    public class Note
    {
        public Guid Id { get; set; } = new Guid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
