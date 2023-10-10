using TDDProject.Models;

namespace TDDUnitTests
{
    public static class UserMockResponse
    {
        public static User GetUserMockResponse()
        {
            return new User()
            {
                UserId = 1,
                Name = "Test",
                Address = "Pune"
            };
        }
    }
}
