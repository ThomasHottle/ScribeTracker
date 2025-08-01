using System.ComponentModel.DataAnnotations;

namespace ScribeTracker.Models
{
    public enum WorkType
    {
        [Display(Name = "Article")]
        Article,

        [Display(Name = "Short Story")]
        ShortStory,

        [Display(Name = "Novel")]
        Novel,

        [Display(Name = "Novella")]
        Novella
    }

    public class Work
    {
        public int WorkId { get; set; }
        public string Title { get; set; } = string.Empty;
        public WorkType Type { get; set; }

        // Foreign Key
        public int PenNameId { get; set; }

        // Navigation
        public PenName PenName { get; set; } = null!;
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
