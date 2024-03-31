using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;

namespace AgiraHire_Backend.Interfaces
{
    public interface IApplicantService {
        OperationResult<Applicant> AddApplicant(Applicant applicant);

        OperationResult<List<Applicant>> GetApplicants();

        OperationResult<Applicant> UpdateApplicant(int applicantId, ApplicantStatus newStatus);
    }


}
