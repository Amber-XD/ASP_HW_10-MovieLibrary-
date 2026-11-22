using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Bll.DTO;
using Bll.Service;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Contracts;
using UI.Vlaidators;
using System.IdentityModel.Tokens.Jwt;

namespace UI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(MovieService _movieService, IValidator<Movie> _moviebalidator, IMapper mapper) : ControllerBase
    {
        private string? CurrentUserId => User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> GetAll()
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var movies = await _movieService.GetAllMoviesByUserIdAsync(CurrentUserId);

            var response = mapper.Map<List<MovieResponse>>(movies);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieResponse>> GetById([FromRoute] int id)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var movie = await _movieService.GetMovieByIdAndUserIdAsync(id, CurrentUserId);

            if (movie == null)
                return NotFound("Movie not found.");

            var response = mapper.Map<MovieResponse>(movie);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<MovieResponse>>> Search([FromQuery] MovieSearchField field, [FromQuery] string value)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var movies = await _movieService.SearchMoviesAsync(
                field,
                value,
                CurrentUserId);

            var response = mapper.Map<List<MovieResponse>>(movies);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized();

            movie.UserId = userId;

            var validation = await _moviebalidator.ValidateAsync(movie);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            await _movieService.AddMovieAsync(movie);

            var response = mapper.Map<MovieResponse>(movie);

            return CreatedAtAction(
                nameof(GetById),
                new { id = movie.Id },
                response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, MovieDto movie)
        {
            if (id != movie.Id)
                return BadRequest("ID mismatch.");

            if (CurrentUserId == null)
                return Unauthorized();

            var existingMovie = await _movieService.GetMovieByIdAndUserIdAsync(
                id,
                CurrentUserId);

            if (existingMovie == null)
                return NotFound("Movie not found.");

            var movieToValidate = mapper.Map<Movie>(movie);
            movieToValidate.UserId = CurrentUserId;

            var validation = await _moviebalidator.ValidateAsync(movieToValidate);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            await _movieService.UpdateMovieAsync(movie, CurrentUserId);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var movie = await _movieService.GetMovieByIdAndUserIdAsync(
                id,
                CurrentUserId);

            if (movie == null)
                return NotFound("Movie not found.");

            await _movieService.DeleteMovieAsync(id, CurrentUserId);

            return NoContent();
        }


    }
}
