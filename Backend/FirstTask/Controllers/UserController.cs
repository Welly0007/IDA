using CoreLayer.Consts;
using CoreLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contracts;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> getUsers(int pageSize = 0, int pageNumber = 0, bool ascending = false, OrderBy orderBy = OrderBy.Id)
        {
            var (users, totalCount) = await _userService.GetUsersAsync(pageSize, pageNumber, ascending, orderBy);
            return Ok(new { items = users, totalCount = totalCount });
        }

        [HttpGet("search")]
        public async Task<IActionResult> searchUsersByName(string searchQuery)
        {
            var result = await _userService.searchUsersByName(searchQuery);
            var (users, totalCount) = result;
            return Ok(new { items = users, totalCount = totalCount });
        }

        [HttpPost("add")]
        public async Task<IActionResult> addUser(UserSaveDto userDto)
        {
            var newUser = await _userService.addUserAsync(userDto);
            return Ok(userDto);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> updateUser(int id, [FromBody] UserSaveDto userSaveDto)
        {
            var updatedUser = await _userService.updateUserAsync(id, userSaveDto);
            return Ok(updatedUser);
        }

        [HttpPut("updatePassword/{id}")]
        public async Task<IActionResult> updatePassword(int id, bool reset = false, string oldPassword = "", string newPassword = "")
        {
            var result = await _userService.updatePasswordAsync(id, oldPassword, newPassword, reset);
            if (!result)
            {
                return BadRequest("Failed to update password");
            }
            return Ok("Password updated successfully");
        }
    }
}
