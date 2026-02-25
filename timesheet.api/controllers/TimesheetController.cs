using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/timesheet")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService timesheetService;
        private readonly IValidator<CreateTimesheetRequestDto> validator;

        public TimesheetController(
            ITimesheetService timesheetService,
            IValidator<CreateTimesheetRequestDto> validator
        )
        {
            this.timesheetService = timesheetService;
            this.validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimesheet([FromBody] CreateTimesheetRequestDto dto)
        {
            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Type = "https://example.com/validation-error",
                };

                return BadRequest(problemDetails);
            }

            var timesheetId = await timesheetService.CreateTimesheet(dto);

            return Ok(timesheetId);
        }
    }
}
