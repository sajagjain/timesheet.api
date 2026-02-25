using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService reportingService;

        public ReportingController(IReportingService reportingService)
        {
            this.reportingService = reportingService;
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<ReportingResponseDto>> GetEmployeeProductivityReport(
            [FromQuery] int year,
            [FromQuery] int month
        )
        {
            if (month < 1 || month > 12)
            {
                return BadRequest("Month must be between 1 and 12.");
            }

            if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            {
                return BadRequest("Year is out of allowed range.");
            }

            var report = await reportingService.GetEmployeeProductivityReport(year, month);
            return Ok(report);
        }
    }
}
