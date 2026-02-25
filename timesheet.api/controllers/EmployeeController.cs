using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Dtos;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(
            EmployeeService employeeService,
            IValidator<CreateTaskRequestDto> createTaskRequestDtoValidator
        )
        {
            this.employeeService = employeeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(string text)
        {
            var items = this.employeeService.GetEmployees();
            return new ObjectResult(items);
        }
    }
}
