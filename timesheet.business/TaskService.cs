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

        public async Task<AutoCompleteResponseDto> AutoComplete(string searchString)
        {
            var query = db.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(t => t.Name.StartsWith(searchString));
            }

            var results = await query
                .OrderBy(t => t.Name)
                .Select(t => new TaskForAutoComplete(t.Id, t.Name))
                .Take(10)
                .ToListAsync();

            return new AutoCompleteResponseDto(results);
        }
    }
}
