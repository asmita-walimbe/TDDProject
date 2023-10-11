using TDDProject.Models;

namespace TDDProject.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetByIdAsync(int userId);
        public Task<User> AddUserAsync(User user);
    }
}
