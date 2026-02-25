using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using timesheet.business;
using timesheet.model;
using Xunit;

namespace timesheet.business.tests;

public class ReportingServiceTests
{
    [Fact]
    public async System.Threading.Tasks.Task GetEmployeeProductivityReport_AggregatesHoursPerEmployeeWithinMonth()
    {
        var year = 2024;
        var month = 1;

        var db = TestDbContextFactory.CreateDbContext();

        var employee1 = new Employee
        {
            Id = 1,
            Name = "Alice",
            Code = "1",
        };
        var employee2 = new Employee
        {
            Id = 2,
            Name = "Bob",
            Code = "2",
        };

        db.Employees.AddRange(employee1, employee2);

        db.Timesheets.AddRange(
            new Timesheet
            {
                EmployeeId = 1,
                Employee = employee1,
                StartDate = new DateTimeOffset(
                    new DateTime(year, month, 1, 9, 0, 0),
                    TimeSpan.Zero
                ),
                EndDate = new DateTimeOffset(new DateTime(year, month, 1, 17, 0, 0), TimeSpan.Zero),
            },
            new Timesheet
            {
                EmployeeId = 1,
                Employee = employee1,
                StartDate = new DateTimeOffset(
                    new DateTime(year, month, 2, 9, 0, 0),
                    TimeSpan.Zero
                ),
                EndDate = new DateTimeOffset(new DateTime(year, month, 2, 12, 0, 0), TimeSpan.Zero),
            },
            new Timesheet
            {
                EmployeeId = 2,
                Employee = employee2,
                StartDate = new DateTimeOffset(
                    new DateTime(year, month, 3, 10, 0, 0),
                    TimeSpan.Zero
                ),
                EndDate = new DateTimeOffset(new DateTime(year, month, 3, 18, 0, 0), TimeSpan.Zero),
            }
        );

        await db.SaveChangesAsync();

        var service = new ReportingService(db);

        var report = await service.GetEmployeeProductivityReport(year, month);

        report.records.Should().HaveCount(2);

        var alice = report.records.Single(r => r.UserId == 1);
        var bob = report.records.Single(r => r.UserId == 2);

        alice.UserName.Should().Be("Alice");
        bob.UserName.Should().Be("Bob");

        alice.TotalHoursWorked.Should().Be(11);
        bob.TotalHoursWorked.Should().Be(8);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetEmployeeProductivityReport_ExcludesTimesheetsOutsideMonthWindow()
    {
        var year = 2024;
        var month = 1;

        var db = TestDbContextFactory.CreateDbContext();

        var employee = new Employee
        {
            Id = 1,
            Name = "Alice",
            Code = "1",
        };
        db.Employees.Add(employee);

        var from = new DateTimeOffset(new DateTime(year, month, 1), TimeSpan.Zero);
        var to = from.AddMonths(1);

        db.Timesheets.AddRange(
            new Timesheet
            {
                EmployeeId = 1,
                Employee = employee,
                StartDate = from.AddMinutes(-1),
                EndDate = from.AddHours(8),
            },
            new Timesheet
            {
                EmployeeId = 1,
                Employee = employee,
                StartDate = from.AddHours(1),
                EndDate = from.AddHours(9),
            },
            new Timesheet
            {
                EmployeeId = 1,
                Employee = employee,
                StartDate = to,
                EndDate = to.AddHours(8),
            }
        );

        await db.SaveChangesAsync();

        var service = new ReportingService(db);

        var report = await service.GetEmployeeProductivityReport(year, month);

        report.records.Should().HaveCount(1);

        var record = report.records.Single();
        record.TotalHoursWorked.Should().Be(8);
    }
}
