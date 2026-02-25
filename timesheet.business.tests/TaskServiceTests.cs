using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using timesheet.business;
using timesheet.model;
using Xunit;

namespace timesheet.business.tests;

public class TaskServiceTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async System.Threading.Tasks.Task AutoComplete_WithNullOrWhitespace_ReturnsAllTasksCappedAtTen(
        string search
    )
    {
        // Arrange
        var db = TestDbContextFactory.CreateDbContext();

        for (var i = 1; i <= 15; i++)
        {
            db.Tasks.Add(new model.Task { Id = i, Name = $"Task{i:00}" });
        }

        await db.SaveChangesAsync();

        var service = new TaskService(db);

        // Act
        var response = await service.AutoComplete(search);

        // Assert
        response.results.Should().HaveCount(10);
    }

    [Fact]
    public async System.Threading.Tasks.Task AutoComplete_FiltersByPrefixAndOrdersByName()
    {
        // Arrange
        var db = TestDbContextFactory.CreateDbContext();

        db.Tasks.AddRange(
            new model.Task { Name = "Alpha" },
            new model.Task { Name = "Alpine" },
            new model.Task { Name = "Beta" }
        );

        await db.SaveChangesAsync();

        var service = new TaskService(db);

        // Act
        var response = await service.AutoComplete("Al");

        // Assert
        response
            .results.Select(r => r.Name)
            .Should()
            .ContainInOrder("Alpha", "Alpine")
            .And.HaveCount(2);
    }

    [Fact]
    public async System.Threading.Tasks.Task AutoComplete_OrdersAllMatchingTasksAlphabetically()
    {
        // Arrange
        var db = TestDbContextFactory.CreateDbContext();

        db.Tasks.AddRange(
            new model.Task { Name = "Zeta" },
            new model.Task { Name = "Alpha" },
            new model.Task { Name = "Gamma" }
        );

        await db.SaveChangesAsync();

        var service = new TaskService(db);

        // Act
        var response = await service.AutoComplete(string.Empty);

        // Assert
        response.results.Select(r => r.Name).Should().ContainInOrder("Alpha", "Gamma", "Zeta");
    }
}
