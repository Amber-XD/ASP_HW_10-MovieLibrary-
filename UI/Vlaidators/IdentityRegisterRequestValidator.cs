using UI.Contracts;
using FluentValidation;

namespace UI.Vlaidators
{
    public class IdentityRegisterRequestValidator : AbstractValidator<IdentityRegisterRequest>
    {
        public IdentityRegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");

            //RuleFor(x => x.Phone)
            //    .Matches(@"^\+?\d{10,15}$");

            //RuleFor(x => x.PhotoPath)
            //    .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            //    .WithMessage("Invalid URL format.");

            //RuleFor(x => x.Age)
            //    .InclusiveBetween(0, 120)
            //    .WithMessage("Age must be between 0 and 120.");
        }
    }
}
