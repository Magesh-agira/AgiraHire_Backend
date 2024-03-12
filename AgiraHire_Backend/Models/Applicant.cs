using System.ComponentModel.DataAnnotations.Schema;

namespace AgiraHire_Backend.Models
{
    public class Applicant
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("Opportunity_Id")]
        public int OpportunityId { get; set; }
        
        public DateTime AppliedDate { get; set; }
        public int? ReferredId { get; set; }
        public string AddedBy { get; set; }
        public ApplicantStatus Status { get; set; }

//      
    }

    public enum ApplicantStatus
    {
        UnderReview,
        Rejected,
        Selected
    }


}
