using System;
using FluentAssertions;
using timesheet.business.Dtos;
using Xunit;

namespace timesheet.business.tests;

public class CreateTimesheetRequestDtoExtensionsTests
{
    [Fact]
    public void ToEntity_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new CreateTimesheetRequestDto
        {
            EmployeeId = 5,
            TaskId = 10,
            StartDate = new DateTimeOffset(new DateTime(2024, 2, 1, 9, 0, 0), TimeSpan.Zero),
            EndDate = new DateTimeOffset(new DateTime(2024, 2, 1, 17, 0, 0), TimeSpan.Zero)
        };

        // Act
        var entity = dto.ToEntity();

        // Assert
        entity.EmployeeId.Should().Be(dto.EmployeeId);
        entity.TaskId.Should().Be(dto.TaskId);
        entity.StartDate.Should().Be(dto.StartDate);
        entity.EndDate.Should().Be(dto.EndDate);
    }
}

