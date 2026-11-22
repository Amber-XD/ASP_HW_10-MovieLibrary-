using AutoMapper;
using Bll.DTO;
using UI.Contracts;

namespace UI.MapperProfile
{
    public class RequestResponseProfile : Profile
    {
        public RequestResponseProfile()
        {
            CreateMap<IdentityRegisterRequest, UserDto>();
            CreateMap<IdentityLoginRequest, LoginDto>();
        }
    }
}