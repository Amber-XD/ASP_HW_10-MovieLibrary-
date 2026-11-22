using System;
using System.Collections.Generic;
using System.Text;
using DLL.Context;
using DLL.Queries;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public class MovieRepository(AppDbContext context) : IRepository<Movie>
    {
        public async Task CreateAsync(Movie entity) => await context.Movies.AddAsync(entity);

        public async Task DeleteAsync(Movie entity) => context.Movies.Remove(entity);

        public async Task DeleteByIdAndUserIdAsync(int id, string userId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(
                m => m.Id == id && m.UserId == userId);

            if (movie != null)
                context.Movies.Remove(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync() => await context.Movies.ToListAsync();

        public List<Movie> SearchByFilter(MovieFilter movieFilter)
        {
            var query = context.Movies.AsQueryable();

            if (movieFilter.Id.HasValue)
            {
                query = query.Where(m => m.Id == movieFilter.Id.Value);
            }

            if (!string.IsNullOrEmpty(movieFilter.Name))
            {
                query = query.Where(m => m.Name.Contains(movieFilter.Name));
            }

            if (!string.IsNullOrEmpty(movieFilter.Genre))
            {
                query = query.Where(m => m.Genre == movieFilter.Genre);
            }

            if (movieFilter.ReleaseYear.HasValue)
            {
                query = query.Where(m => m.ReleaseDate.Year == movieFilter.ReleaseYear.Value);
            }

            if (!string.IsNullOrEmpty(movieFilter.Description))
            {
                query = query.Where(m => m.Description.Contains(movieFilter.Description));
            }

            if (!string.IsNullOrEmpty(movieFilter.Keyword))
            {
                query = query.Where(m => m.Keyword.Contains(movieFilter.Keyword));
            }

            if (!string.IsNullOrEmpty(movieFilter.Country))
            {
                query = query.Where(m => m.Country == movieFilter.Country);
            }

            return query.ToList();
        }

        public async Task<Movie?> GetByIdAsync(int id) => await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task<IEnumerable<Movie>> GetAllByUserIdAsync(string userId) =>
            await context.Movies
                .Where(m => m.UserId == userId)
                .ToListAsync();

        public async Task<Movie?> GetByIdAndUserIdAsync(int id, string userId) =>
            await context.Movies
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        public async Task UpdateAsync(Movie entity, string userId)
        {
            var existingMovie = await context.Movies.FirstOrDefaultAsync(
                m => m.Id == entity.Id && m.UserId == userId);

            if (existingMovie != null)
            {
                existingMovie.Name = entity.Name;
                existingMovie.Description = entity.Description;
                existingMovie.ReleaseDate = entity.ReleaseDate;
                existingMovie.Genre = entity.Genre;
                existingMovie.Director = entity.Director;
                existingMovie.PosterUrl = entity.PosterUrl;
                existingMovie.MovieUrl = entity.MovieUrl;
                existingMovie.Keyword = entity.Keyword;
                existingMovie.Country = entity.Country;
            }
        }


    }
}
