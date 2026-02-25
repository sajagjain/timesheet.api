using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Dtos;
using timesheet.business.Dtos;

namespace timesheet.api.controllers
{
    [Route("api/v1/reports")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly ReportingService reportingService;
        private readonly IValidator<CreateTaskRequestDto> createTaskRequestDtoValidator;

        public ReportingController(
            ReportingService reportingService,
            IValidator<CreateTaskRequestDto> createTaskRequestDtoValidator
        )
        {
            this.reportingService = reportingService;
            this.createTaskRequestDtoValidator = createTaskRequestDtoValidator;
        }

        [HttpPost]
        public Task<ReportingResponseDto> GetEmployeeProductivityReport()
        {
            return reportingService.GetEmployeeProductivityReport();
        }
    }
}
