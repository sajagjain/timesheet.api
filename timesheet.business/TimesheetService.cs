using System;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class TimesheetService(TimesheetDb _timesheetDbContext) : ITimesheetService
    {
        public async Task<int> CreateTimesheet(CreateTimesheetRequestDto dto)
        {
            var timesheet = dto.ToEntity();
            _timesheetDbContext.Timesheets.Add(timesheet);
            await _timesheetDbContext.SaveChangesAsync();

            return timesheet.Id;
        }
    }
}
