using AgiraHire_Backend.Models;

namespace AgiraHire_Backend.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);

        public List<User> GetUsers();
        bool DeleteUser(int UserId);

    }
}
