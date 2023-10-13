using TDDProject.Interfaces;
using TDDProject.Models;

namespace TDDProject.Services
{
    public class UserService : IUserService
    {
        public async Task<User> AddUserAsync(User user)
        {
            user.Id = 1;
            return user;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            // will get the data from db later.
            var users = new List<User>();
            users.Add(new User(1, "Test", "Pune"));
            users.Add(new User(2, "Test2", "Chennai"));
            var response = users.FirstOrDefault(x => x.Id == userId);
            return response;
        }
    }
}
