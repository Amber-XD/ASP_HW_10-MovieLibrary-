using FluentValidation;
using Domain.Models;

namespace UI.Vlaidators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Movie name is required.")
                .MaximumLength(100)
                .WithMessage("Movie name cannot exceed 100 characters.");

            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(2000)
                .WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(m => m.ReleaseDate)
                .NotEmpty()
                .WithMessage("Release date is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Release date cannot be in the future.");

            RuleFor(m => m.Genre)
                .NotEmpty()
                .WithMessage("Genre is required.")
                .MaximumLength(50)
                .WithMessage("Genre cannot exceed 50 characters.");

            RuleFor(m => m.Director)
                .NotEmpty()
                .WithMessage("Director is required.")
                .MaximumLength(100)
                .WithMessage("Director cannot exceed 100 characters.");

            RuleFor(m => m.Keyword)
                .NotEmpty()
                .WithMessage("Keyword is required.")
                .MaximumLength(100)
                .WithMessage("Keyword cannot exceed 100 characters.");

            RuleFor(m => m.Country)
                .NotEmpty()
                .WithMessage("Country is required.")
                .MaximumLength(100)
                .WithMessage("Country cannot exceed 100 characters.");
        }


    }
}
