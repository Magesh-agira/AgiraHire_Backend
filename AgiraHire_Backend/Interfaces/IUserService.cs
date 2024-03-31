using AgiraHire_Backend.Models;
using AgiraHire_Backend.Response;
using System.Collections.Generic;

namespace AgiraHire_Backend.Interfaces  //it defines the contrect for the service 
{
    public interface IUserService
    {
        OperationResult<User> AddUser(User user);
        OperationResult<List<User>> GetUsers();
        OperationResult<bool> DeleteUser(int UserId);
    }
}
    