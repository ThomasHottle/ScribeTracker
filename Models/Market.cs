namespace ScribeTracker.Models
{
    public class Market
    {
        public int MarketId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; }

        // Navigation
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
