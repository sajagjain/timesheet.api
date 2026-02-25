using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface ITimesheetService
    {
        Task<int> CreateTimesheet(CreateTimesheetRequestDto dto);
    }
}
