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
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Test",
                    Address = "Pune"
                },
                new User()
                {
                    Id = 2,
                    Name  ="Test 2",
                    Address = "Chennai"
                }
            };
            var response = users.FirstOrDefault(x => x.Id == userId);
            return response;
        }
    }
}
