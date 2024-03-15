using System.ComponentModel.DataAnnotations;

namespace AgiraHire_Backend.Models
{
    public class opportunity
    {
        [Key]
        public int Opportunity_Id { get; set; }
        public string? Position { get; set; }
        public string? Location { get; set; }
        public string? Employment_Type { get; set; }
        public string? Qualification { get; set; }
        public string? Salary { get; set; }
        public DateTime? Date_Posted { get; set; }
        public int? No_Of_Openings { get; set; }
        public OpportunityStatus? Status { get; set; }
        public bool? IsDeleted { get; set; } = false;

      
    }
    public enum OpportunityStatus
    {
        Open,
        Closed,
        Hold
    }
}
