using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IApplicantService { 
        public Applicant AddApplicant(Applicant applicant);

        public List<Applicant> GetApplicants();

        Applicant UpdateApplicant(int applicantId, ApplicantStatus newStatus);
    }
}
