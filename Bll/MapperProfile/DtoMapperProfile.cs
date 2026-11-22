
using AutoMapper;
using Bll.DTO;
using Domain.Models;

namespace Bll.MapperProfile
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.OldMovie, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Age, opt => opt.Ignore());

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.ReleaseYear,
                    opt => opt.MapFrom(src => src.ReleaseDate.Year));
        }
    }
}
