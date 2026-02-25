using System;
using System.Threading.Tasks;
using timesheet.business.Dtos;
using timesheet.business.Interfaces;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class TimesheetService(TimesheetDb db) : ITimesheetService
    {
        public async Task<int> CreateTimesheet(CreateTimesheetRequestDto dto)
        {
            var timesheet = dto.ToEntity();
            db.Timesheets.Add(timesheet);
            await db.SaveChangesAsync();

            return timesheet.Id;
        }
    }
}
