using System.ComponentModel.DataAnnotations;

namespace Taskify.Api.DTOs.JournalDtos
{
    public class CreateJournalEntryDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public int Mood { get; set; } // 1–5
    }
}
