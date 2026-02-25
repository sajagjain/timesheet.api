using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timesheet.business.Dtos
{
    public record ReportingResponseDto(List<EmployeeReportingDto> records);

    public record EmployeeReportingDto(int UserId, string UserName, double TotalHoursWorked);
}
