using AutoFixture;
using TDDProject.Models;

namespace TDDUnitTests
{
    public static class UserAutoFixture
    {
        public static Fixture GetUserFixture(Fixture _fixture)
        {
            _fixture.Customize<User>(c =>
            {
                c.Without(c => c.UserId);
                c.With(c => c.Name, _fixture.Create<string>());
                c.With(c => c.Address, _fixture.Create<string>());
                return c;
            });
            return _fixture;
        }

    }
}
