using MongoDB.Driver;
using TDDProject.Interfaces;
using TDDProject.Models;
using TDDProject.MongoDB;

namespace TDDProject.Services
{
    public class UserService : IUserService
    {
        private readonly MongoDBContext _context;
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(MongoDBContext context)
        {
            _context = context;
            _usersCollection = _context._collections;
        }
        public async Task<User> AddAsync(User user)
        {
            Random getrandom = new Random();
            user.UserId = getrandom.Next(1, 200);
            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(int id, User updatedUser)
        {
            var filter = Builders<User>.Filter
                .Eq(user => user.UserId, updatedUser.UserId);
            var update = Builders<User>.Update
                .Set(user => user.Name, updatedUser.Name)
                .Set(user => user.Address, updatedUser.Address);

            await _usersCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<User>> GetAllAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetByIdAsync(int userId)
        {
            var response = await _usersCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return response;
        }

        public async Task DeleteAsync(int id) =>
            await _usersCollection.DeleteOneAsync(x => x.UserId == id);
    }
}
