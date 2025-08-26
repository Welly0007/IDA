using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreLayer.Consts;
using CoreLayer.Dtos;
using CoreLayer.Interfaces;
using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Contracts;

namespace ServiceLayer.Services.Implementation
{
    public class UserService : IUserService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(IEnumerable<UserDto> users, int totalCount)> GetUsersAsync(int pageSize, int pageNumber, bool ascending, OrderBy orderBy)
        {
            IQueryable<User> query = _unitOfWork.CreateRepository<User>().getQueryable();
            var totalCount = await query.CountAsync();
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = ascending ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id);
                    break;
                case OrderBy.Name:
                    query = ascending ? query.OrderBy(u => u.Emp.Ctzn.Name) : query.OrderByDescending(u => u.Emp.Ctzn.Name);
                    break;
                case OrderBy.UserName:
                    query = ascending ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                    break;
            }
            if (pageSize > 0 && pageNumber > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            var users = await query.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();

            return (users, totalCount);
        }
        public async Task<(IEnumerable<UserDto> users, int totalCount)> searchUsersByName(string searchQuery)
        {
            IQueryable<User> query = _unitOfWork.CreateRepository<User>().getQueryable();

            query = query.Where(u => u.Emp.Ctzn.Name.StartsWith(searchQuery) );
            var totalCount = await query.CountAsync();
            var users = await query.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return (users, totalCount);
        }
        public async Task<User> addUserAsync(UserSaveDto userDto)
        {
            var newCitizen = new Citizen
            {
                Name = userDto.EmpName,
                NatId = userDto.NatId
            };
            var newEmp = new Emp
            {
                DeptId = userDto.DeptId,
                Ctzn = newCitizen
            };

            _unitOfWork.CreateRepository<Citizen>().add(newCitizen);
            _unitOfWork.CreateRepository<Emp>().add(newEmp);
            var newUser = new User
            {
                Emp = newEmp,
                UserName = userDto.UserName,
                ExtClctr = userDto.ExtClctr,
                Stopped = userDto.Stopped,
                Password = "12345678",
                GroupUsers = userDto.WorkGroupIds.Select(groupId => new GroupUser { GroupId = groupId }).ToList()
            };
            _unitOfWork.CreateRepository<User>().add(newUser);
            await _unitOfWork.CompleteAsync();
            return newUser;
        }


        public async Task<User> updateUserAsync(int id, UserSaveDto userDto)
        {
            var userRepository = _unitOfWork.CreateRepository<User>();
            var currUser =  await userRepository.getByIdAsync(id);
            if (currUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            currUser = _mapper.Map(userDto, currUser);
            currUser.GroupUsers.Clear();
            foreach (var groupId in userDto.WorkGroupIds)
            {
                currUser.GroupUsers.Add(new GroupUser { GroupId = groupId });
            }
            userRepository.update(currUser);
            await _unitOfWork.CompleteAsync();
            return currUser;

        }
        public async Task<bool> updatePasswordAsync(int id, string oldPassword, string newPassword, bool reset)
        {
            var user =  await _unitOfWork.CreateRepository<User>().getByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            if (reset)
            {
                user.Password = "12345678";
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else if(user.Password == oldPassword)
            {
                user.Password = newPassword;
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}
