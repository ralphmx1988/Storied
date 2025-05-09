using FluentValidation;
using Storied.Application.Features.Person.Commands;
using System.Globalization;

namespace Storied.Application.Validators
{
    /// <summary>  
    /// Validator for the <see cref="UpdatePersonCommand"/>.  
    /// </summary>  
    public sealed class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="UpdatePersonValidator"/> class.  
        /// </summary>  
        public UpdatePersonValidator()
        {
            RuleFor(x => x.BirthDate)
                .Must(BeAValidDate)
                .WithMessage("BirthDate is required and must be in the format 'yyyy-MM-dd'.");

            RuleFor(x => x.BirthLocation)
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Birth Location is required must be between 3 and 50 characters long.")
                .When(x => !string.IsNullOrEmpty(x.BirthLocation));
        }

        /// <summary>  
        /// Validates whether the provided date string is in the 'yyyy-MM-dd' format.  
        /// </summary>  
        /// <param name="date">The date string to validate.</param>  
        /// <returns><c>true</c> if the date is valid; otherwise, <c>false</c>.</returns>  
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
}
