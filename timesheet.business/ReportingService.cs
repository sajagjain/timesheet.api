using System;
using System.Linq;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class ReportingService(TaskService taskService) : IReportingService
    {
        public Task<ReportingResponseDto> GetEmployeeProductivityReport()
        {
            return taskService.GetEmployeeProductivityReport();
        }
    }
}
