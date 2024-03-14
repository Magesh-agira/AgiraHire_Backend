using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);

        bool DeleteUser(int UserId);

    }
}
