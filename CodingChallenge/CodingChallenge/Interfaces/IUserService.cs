using CodingChallenge.Models;

namespace CodingChallenge.Interfaces
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<List<User>> GetAllUser();
        Task<User> GetUser(int userId);
        Task<bool> RemoveUser(User user);
        Task<User> UpdateUser(User user);
    }
}
