using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class ReportingService(TimesheetDb db) : IReportingService
    {
        public async Task<ReportingResponseDto> GetEmployeeProductivityReport(int year, int month)
        {
            var from = new DateTimeOffset(new DateTime(year, month, 1), TimeSpan.Zero);
            var to = from.AddMonths(1);

            var records = await db
                .Timesheets.Include(t => t.Employee)
                .Where(t => t.StartDate >= from && t.StartDate < to)
                .GroupBy(t => t.EmployeeId)
                .Select(g => new EmployeeReportingDto(
                    g.Key,
                    g.Select(t => t.Employee).FirstOrDefault().Name ?? "Unknown",
                    g.Sum(t => EF.Functions.DateDiffHour(t.StartDate, t.EndDate))
                ))
                .ToListAsync();

            return new ReportingResponseDto(records);
        }
    }
}
