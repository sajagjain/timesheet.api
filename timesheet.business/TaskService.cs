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
    public class TaskService : ITaskService
    {
        private readonly TimesheetDb db;

        public TaskService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public async Task<int> CreateTask(CreateTaskRequestDto dto)
        {
            model.Task t = CreateTaskObjectFromDto(dto);
            db.Add(t);
            await db.SaveChangesAsync();
            return t.Id;
        }

        private static model.Task CreateTaskObjectFromDto(CreateTaskRequestDto dto)
        {
            return new()
            {
                Name = dto.TaskName,
                Description = dto.TaskDescription,
                TaskType = dto.Type,
            };
        }

        public async Task<ReportingResponseDto> GetEmployeeProductivityReport()
        {
            var records = db
                .Tasks.Include(t => t.Employee)
                .GroupBy(t => t.EmployeeId)
                .Select(g => new EmployeeReportingDto(
                    g.Key,
                    g.Select(t => t.Employee).FirstOrDefault().Name ?? "Unknown",
                    g.Select(t => (t.EndDate - t.StartDate).Hours).Sum()
                ))
                .ToList();

            return new ReportingResponseDto(records);
        }

        public async Task<AutoCompleteResponseDto> AutoComplete(string searchString)
        {
            var results = await db
                .Tasks.Where(t => t.Name.StartsWith(searchString))
                .Select(t => new TaskForAutoComplete(t.Id, t.Name))
                .Take(10)
                .ToListAsync();
            return new AutoCompleteResponseDto(results);
        }
    }
}
