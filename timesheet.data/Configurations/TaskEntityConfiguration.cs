using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using timesheet.model;

namespace timesheet.data.Configurations
{
    internal class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasOne<Employee>().WithMany().HasForeignKey(t => t.EmployeeId);
        }
    }
}
