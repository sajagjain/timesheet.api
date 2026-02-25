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
                .Timesheets.Where(t => t.StartDate >= from && t.StartDate < to)
                .Include(t => t.Employee)
                .ToListAsync();

            var mappedEmployeeReportingDto = records
                .GroupBy(t => t.EmployeeId)
                .Select(g => new EmployeeReportingDto(
                    g.Key,
                    g.Select(t => t.Employee).FirstOrDefault().Name ?? "Unknown",
                    g.Sum(t => (t.EndDate - t.StartDate).Hours)
                ))
                .ToList();

            return new ReportingResponseDto(mappedEmployeeReportingDto);
        }
    }
}
