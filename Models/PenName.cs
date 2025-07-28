namespace ScribeTracker.Models
{
    public class PenName
    {
        public int PenNameId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Work> Works { get; set; } = new List<Work>();
    }
}
