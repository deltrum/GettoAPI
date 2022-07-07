using AutoMapper;
using GettoAPI.Dtos;
using GettoAPI.Models;

namespace GettoAPI.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<Member, MemberRDto>();
            CreateMap<MemberCDto, Member>();
            CreateMap<MemberUDto, Member>();
        }
    }
}