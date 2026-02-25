using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;

namespace timesheet.api.controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("autocomplete")]
        public async Task<AutoCompleteResponseDto> AutoComplete([FromQuery] string? searchString)
        {
            return await taskService.AutoComplete(searchString);
        }
    }
}
