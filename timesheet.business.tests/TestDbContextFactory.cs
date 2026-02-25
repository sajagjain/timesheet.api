using System;
using Microsoft.EntityFrameworkCore;
using timesheet.data;

namespace timesheet.business.tests;

public static class TestDbContextFactory
{
    public static TimesheetDb CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TimesheetDb>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TimesheetDb(options);
    }
}

