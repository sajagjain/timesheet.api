using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface ITaskService
    {
        Task<AutoCompleteResponseDto> AutoComplete(string searchString);
        Task<int> CreateTask(CreateTaskRequestDto dto);
        Task<ReportingResponseDto> GetEmployeeProductivityReport();
    }
}
