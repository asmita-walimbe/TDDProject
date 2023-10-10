using Microsoft.AspNetCore.Mvc;
using TDDProject.Models;

namespace TDDProject.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("getById/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var users = new List<User>()
            {
                new User()
                {
                    UserId = 1,
                    Name = "Test",
                    Address = "Pune"
                },
                new User()
                {
                    UserId = 2,
                    Name  ="Test 2",
                    Address = "Chennai"
                }
            };
            var user = users.Select(x => x.UserId = userId);
            return Ok(user);
        }
    }
}
