using TDDProject.Models;

namespace TDDProject.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetByIdAsync(int Id);
        public Task<List<User>> GetAllAsync();
        public Task<User> AddAsync(User user);
        public Task UpdateAsync(int id, User user);
        public Task DeleteAsync(int id);
    }
}
