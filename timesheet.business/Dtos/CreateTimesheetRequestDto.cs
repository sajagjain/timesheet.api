using System;
using timesheet.model;

namespace timesheet.business.Dtos
{
    public class CreateTimesheetRequestDto
    {
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }

    public static class CreateTimesheetRequestDtoExtensions
    {
        public static Timesheet ToEntity(this CreateTimesheetRequestDto dto)
        {
            return new Timesheet
            {
                //TODO: Taking employee id from dto, eventually should be taken from auth
                EmployeeId = dto.EmployeeId,
                TaskId = dto.TaskId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
        }
    }
}
