
using CoreLayer.Consts;
using CoreLayer.Dtos;
using CoreLayer.Models;

namespace ServiceLayer.Services.Contracts
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> users, int totalCount)> GetUsersAsync(int pageSize, int pageNumber, bool ascending, OrderBy orderBy);
        Task<(IEnumerable<UserDto> users, int totalCount)> searchUsersByName(string searchQuery);
        Task<User> addUserAsync(UserSaveDto userDto);
        Task<User> updateUserAsync(int id, UserSaveDto userDto);
        Task<bool> updatePasswordAsync(int id,string oldPassword, string newPassword, bool reset);
    }
}
