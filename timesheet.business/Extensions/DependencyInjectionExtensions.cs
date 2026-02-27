using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using timesheet.business.Interfaces;

namespace timesheet.business.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTimesheetServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITimesheetService, TimesheetService>();
            services.AddScoped<IReportingService, ReportingService>();

            return services;
        }
    }
}
