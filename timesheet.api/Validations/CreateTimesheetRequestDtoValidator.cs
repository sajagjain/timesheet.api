using System;
using FluentValidation;
using timesheet.business.Dtos;

namespace timesheet.api.Validations
{
    public class CreateTimesheetRequestDtoValidator : AbstractValidator<CreateTimesheetRequestDto>
    {
        public CreateTimesheetRequestDtoValidator()
        {
            RuleFor(t => t.StartDate)
                .Must(BeWithinAllowedDateWindow)
                .WithMessage("Date cannot be in the future or older than 1 month from today");

            RuleFor(t => t.EndDate)
                .Must(BeWithinAllowedDateWindow)
                .WithMessage("Date cannot be in the future or older than 1 month from today");

            RuleFor(t => t.EmployeeId)
                .GreaterThan(0)
                .WithMessage("EmployeeId must be a positive value");

            RuleFor(t => t.TaskId).GreaterThan(0).WithMessage("TaskId must be a positive value");
        }

        private static bool BeWithinAllowedDateWindow(DateTimeOffset date)
        {
            var today = DateTimeOffset.UtcNow.Date;
            var minDate = today.AddMonths(-1);

            var targetDate = date.Date;

            return targetDate >= minDate && targetDate <= today;
        }
    }
}
