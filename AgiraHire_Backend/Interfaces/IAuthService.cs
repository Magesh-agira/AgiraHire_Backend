using AgiraHire_Backend.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace AgiraHire_Backend.Interfaces
{
    public interface IAuthService
    {
        User AddUser( User user);
        string Login(LoginRequest loginRequest);
    }
}
