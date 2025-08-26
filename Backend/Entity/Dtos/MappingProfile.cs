using AutoMapper;
using CoreLayer.Models;

namespace CoreLayer.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.Emp.Ctzn.Name))
                .ForMember(dest => dest.NatId, opt => opt.MapFrom(src => src.Emp.Ctzn.NatId))
                .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.Emp.Dept.Name))
                .ForMember(dest => dest.DeptId, opt => opt.MapFrom(src => src.Emp.DeptId))
                .ForMember(dest => dest.WorkGroupIds, opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.GroupId).ToList()))
                .ForMember(dest => dest.WorkGroups, opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.Group.Name).ToList()));

            CreateMap<UserSaveDto, User>()
                .ForPath(dest=> dest.Emp.Ctzn.Name, opt => opt.MapFrom(src => src.EmpName))
                .ForPath(dest => dest.Emp.Ctzn.NatId, opt => opt.MapFrom(src => src.NatId))
                .ForPath(dest => dest.Emp.DeptId, opt => opt.MapFrom(src => src.DeptId))
                .ForMember(dest => dest.GroupUsers, opt => opt.Ignore());
        }
    }
}