using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Bll.DTO;
using DLL.Queries;
using DLL.Repository;
using Domain.Models;

namespace Bll.Service
{
    public class MovieService(MovieRepository _movieRepository, IMapper _mapper)
    {
        public async Task<List<MovieDto>> GetAllMoviesAsync() =>
            _mapper.Map<List<MovieDto>>(await _movieRepository.GetAllAsync());

        public async Task<MovieDto?> GetMovieByIdAsync(int id) =>
            _mapper.Map<MovieDto>(await _movieRepository.GetByIdAsync(id));
        public async Task<List<MovieDto>> GetAllMoviesByUserIdAsync(string userId) =>
            _mapper.Map<List<MovieDto>>(
                await _movieRepository.GetAllByUserIdAsync(userId));

        public async Task<MovieDto?> GetMovieByIdAndUserIdAsync(int id, string userId) =>
            _mapper.Map<MovieDto>(
                await _movieRepository.GetByIdAndUserIdAsync(id, userId));

        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.CreateAsync(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(MovieDto movieDto, string userId)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.UpdateAsync(movie, userId);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id, string userId)
        {
            await _movieRepository.DeleteByIdAndUserIdAsync(id, userId);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task<List<MovieDto>> SearchMoviesAsync(MovieSearchField field, string value, string userId)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<MovieDto>();

            value = value.ToLower();

            var movies = await _movieRepository.GetAllByUserIdAsync(userId);

            var result = movies.Where(m => field switch
            {   
                MovieSearchField.Name => m.Name.ToLower().Contains(value),
                MovieSearchField.Genre => m.Genre.ToLower().Contains(value),
                MovieSearchField.Director => m.Director.ToLower().Contains(value),
                MovieSearchField.Year => m.ReleaseDate.Year.ToString().Contains(value),
                MovieSearchField.Country => m.Country.ToLower().Contains(value),
                MovieSearchField.Keyword => m.Keyword.ToLower().Contains(value),
                _ => false
            }).ToList();

            return _mapper.Map<List<MovieDto>>(result);
        }


    }
}
