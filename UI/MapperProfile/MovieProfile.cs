
using AutoMapper;
using Domain.Models;
using Bll.DTO;
using UI.Contracts;

namespace UI.MapperProfile
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.ReleaseYear,
                opt => opt.MapFrom(src => src.ReleaseDate.Year));

            CreateMap<MovieDto, Movie>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<MovieDto, MovieResponse>();
        }
    }
}
