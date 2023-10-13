using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TDDProject.Interfaces;
using TDDProject.Models;

namespace TDDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly IValidator<User> _validator;

        public UserController(IUserService userService, IValidator<User> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        /// <summary>
        /// Returns data for testing purpose. Will get from db through services later.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        //[Route("getByIdAsync/{userId:int}")]

        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            if (userId == 0)
            {
                return BadRequest("UserId is required");
            }
            var response = await _userService.GetByIdAsync(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        /// <summary>
        /// Returns data for created user for testing purpose. Will get from db through services later.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            var response = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }
    }
}
