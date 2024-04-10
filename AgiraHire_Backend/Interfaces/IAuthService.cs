using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;

namespace AgiraHire_Backend.Interfaces
{
    public interface IAuthService
    {
        OperationResult<List<Role>> GetRoles();

        OperationResult<string> Login(LoginRequest loginRequest);
        OperationResult<Role> AddRole(Role role);
        OperationResult<bool> AssignRoleToUser(AddUserRole obj);

        OperationResult<List<UserRole>> GetUserRoles();


    }
}

