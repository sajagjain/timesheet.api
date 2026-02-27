using System;
using System.Linq;
using timesheet.business.Dtos;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class EmployeeService(TimesheetDb _timesheetDbContext)
    {
        public IQueryable<Employee> GetEmployees()
        {
            return _timesheetDbContext.Employees;
        }
    }
}
