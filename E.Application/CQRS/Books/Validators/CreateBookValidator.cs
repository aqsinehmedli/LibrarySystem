using FluentValidation;
using static E.Application.CQRS.Books.Handlers.Create;

namespace E.Application.CQRS.Books.Validators;

public class CreateBookValidator:AbstractValidator<Command>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required.")
            .Length(3, 100).WithMessage("Author must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.CoverPhoto)
            .NotNull().WithMessage("Cover photo is required.")
            .Must(x => Guid.TryParse(x.ToString(), out _)).WithMessage("Cover photo must be a valid GUID.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Language must be a valid value.");
    }
}
