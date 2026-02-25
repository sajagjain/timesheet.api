using System.Collections.Generic;
using FluentAssertions;
using timesheet.business.Dtos;
using Xunit;

namespace timesheet.business.tests;

public class DtoRecordTests
{
    [Fact]
    public void ReportingResponseDto_ShouldExposeGivenRecords()
    {
        // Arrange
        var items = new List<EmployeeReportingDto>
        {
            new(1, "Alice", 8),
            new(2, "Bob", 5)
        };

        // Act
        var dto = new ReportingResponseDto(items);

        // Assert
        dto.records.Should().BeSameAs(items);
    }

    [Fact]
    public void AutoCompleteResponseDto_ShouldExposeGivenResults()
    {
        // Arrange
        var results = new List<TaskForAutoComplete>
        {
            new(1, "Task A"),
            new(2, "Task B")
        };

        // Act
        var dto = new AutoCompleteResponseDto(results);

        // Assert
        dto.results.Should().BeSameAs(results);
    }
}

