using System;
using System.Threading.Tasks;
using FluentAssertions;
using timesheet.business;
using timesheet.business.Dtos;
using Xunit;

namespace timesheet.business.tests;

public class TimesheetServiceTests
{
    [Fact]
    public async Task CreateTimesheet_ShouldPersistEntityAndReturnId()
    {
        // Arrange
        var db = TestDbContextFactory.CreateDbContext();
        var service = new TimesheetService(db);

        var dto = new CreateTimesheetRequestDto
        {
            EmployeeId = 1,
            TaskId = 2,
            StartDate = new DateTimeOffset(new DateTime(2024, 1, 1, 9, 0, 0), TimeSpan.Zero),
            EndDate = new DateTimeOffset(new DateTime(2024, 1, 1, 17, 0, 0), TimeSpan.Zero)
        };

        // Act
        var id = await service.CreateTimesheet(dto);

        // Assert
        id.Should().BeGreaterThan(0);

        var timesheets = db.Timesheets;
        timesheets.Should().HaveCount(1);

        var entity = await db.Timesheets.FindAsync(id);
        entity.Should().NotBeNull();
        entity!.EmployeeId.Should().Be(dto.EmployeeId);
        entity.TaskId.Should().Be(dto.TaskId);
        entity.StartDate.Should().Be(dto.StartDate);
        entity.EndDate.Should().Be(dto.EndDate);
    }
}

