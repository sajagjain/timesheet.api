using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.business.Interfaces
{
    public interface IReportingService
    {
        Task<ReportingResponseDto> GetEmployeeProductivityReport();
    }
}
