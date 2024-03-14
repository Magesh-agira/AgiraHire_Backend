using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Identity.Data;


namespace AgiraHire_Backend.Interfaces
{
    public interface IAuthService
    {
        
        string Login(Models.LoginRequest loginRequest);

        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);

        

    }
}
