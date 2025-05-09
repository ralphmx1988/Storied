using System.Globalization;
using FluentValidation;
using Storied.Application.Features.Person.Commands;

namespace Storied.Application.Validators;

public sealed class AddPersonValidator : AbstractValidator<AddPersonCommand>
{

   
    public AddPersonValidator()
    {
        var genders = new List<string>() { "Male","Female","Other" };



        RuleFor(x => x.GivenName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Given name is required and must be between 3 and 50 characters long.");

        RuleFor(x => x.SurName)
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Surname  must be between 3 and 50 characters long.")
            .When(x => !string.IsNullOrEmpty(x.SurName));

        RuleFor(x => x.Gender)
            .Must(x => genders.Contains(x))
            .WithMessage("for Gender only use: " + String.Join(", ", genders));


        RuleFor(x => x.BirthDate)
            .Must(BeAValidDate)
            .WithMessage("BirthDate must be in the format 'yyyy-MM-dd'.")
            .When(x => !string.IsNullOrEmpty(x.BirthDate));

        RuleFor(x => x.BirthLocation)
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Birth Location must be between 3 and 50 characters long.")
            .When(x => !string.IsNullOrEmpty(x.BirthLocation));

        RuleFor(x => x.DeathDate)
            .Must(BeAValidDate)
            .WithMessage("DeathDate must be in the format 'yyyy-MM-dd'.")
            .When(x => !string.IsNullOrEmpty(x.DeathDate));


        RuleFor(x => x.DeathLocation)
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Death Location must be between 3 and 50 characters long.")
            .When(x => !string.IsNullOrEmpty(x.DeathLocation));
    }

    private bool BeAValidDate(string? date)
    {
        return DateTime.TryParseExact(
            date,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _
        );
    }
}

