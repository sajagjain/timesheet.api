using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.business.Dtos;

namespace timesheet.api.controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService taskService;
        private readonly IValidator<CreateTaskRequestDto> createTaskRequestDtoValidator;

        public TaskController(
            TaskService taskService,
            IValidator<CreateTaskRequestDto> createTaskRequestDtoValidator
        )
        {
            this.taskService = taskService;
            this.createTaskRequestDtoValidator = createTaskRequestDtoValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestDto dto)
        {
            var validationResult = createTaskRequestDtoValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var taskId = await taskService.CreateTask(dto);

            return Ok(taskId);
        }

        [HttpGet("/autocomplete/{searchString}")]
        public async Task<AutoCompleteResponseDto> AutoComplete([FromRoute] string? searchString)
        {
            return await taskService.AutoComplete(searchString);
        }
    }
}
