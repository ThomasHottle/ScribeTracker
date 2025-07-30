using System.ComponentModel.DataAnnotations;

namespace ScribeTracker.Models
{
    public enum SubmissionStatus
    {
        Submitted,
        Pending,
        Accepted,
        Rejected,
        Published
    }
    
    public class Submission
    {
        public int SubmissionId { get; set; }

        // Foreign Keys
        public int WorkId { get; set; }
        public int MarketId { get; set; }

        public DateTime SubmissionDate { get; set; }
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Submitted;
        
        [DataType(DataType.Currency)]
        [Display(Name = "Payment Received")] 
        public decimal? Payment { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public Work Work { get; set; } = null!;
        public Market Market { get; set; } = null!;

        [Display(Name = "Self-Published")]
        public bool SelfPub { get; set; } = false;
    }
}
