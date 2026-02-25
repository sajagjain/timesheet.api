using System;
using FluentValidation;
using FluentValidation.Results;
using timesheet.business.Dtos;

namespace timesheet.api.Validations
{
    public class CreateTaskRequestDtoValidator : AbstractValidator<CreateTaskRequestDto>
    {
        public override ValidationResult Validate(ValidationContext<CreateTaskRequestDto> context)
        {
            RuleFor(t => t.StartDate)
                .InclusiveBetween(DateTimeOffset.UtcNow.AddDays(-30), DateTimeOffset.UtcNow);
            RuleFor(t => t.EndDate)
                .InclusiveBetween(DateTimeOffset.UtcNow.AddDays(-30), DateTimeOffset.UtcNow);
            return base.Validate(context);
        }
    }
}
