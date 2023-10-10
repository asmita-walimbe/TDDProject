using TDDProject.Models;

namespace TDD_IntegrationTests
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
