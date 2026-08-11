using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Simulation.Entities;

public class SimulationTests
{
    //[Theory]
    //[InlineData(1)]
    //public void Creation_EveryCyclistIsInSameGroup(int cyclistCount)
    //{
    //    var cyclists = new List<Domain.Simulation.Entities.Cyclist>();
    //    for (int i = 0; i < cyclistCount; i++)
    //        cyclists.Add(new Domain.Simulation.Entities.Cyclist());

    //    var simulation = new Domain.Simulation.Entities.Simulation(1, cyclists);
    //}

    [Fact]
    public void Advance_WithSingleTickRace_AndSingleCyclist_IsFinishedIsTrue()
    {
        // Arrange
        var group = RaceGroupBuilder.Default()
                                    .WithSpeed(10)
                                    .Build();
        var cyclists = new List<Domain.Simulation.Entities.Cyclist>
        {
            CyclistBuilder.Default().WithGroup(group).Build(),
        };

        var simulation = new Domain.Simulation.Entities.Simulation(0.01, cyclists, new List<RaceGroup> { group });

        // Act
        simulation.Advance();

        // Assert
        Assert.True(simulation.IsFinished);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(150)]
    [InlineData(200)]
    [InlineData(300)]
    public void Advance_WithMultipleTickRace_AndSingleCyclist_IsFinishedIsTrueAfterNecessaryTicks(int raceDistanceKm)
    {
        // Arrange
        var speed = 10;
        var group = RaceGroupBuilder.Default()
                                    .WithSpeed(10)
                                    .Build();
        var cyclists = new List<Domain.Simulation.Entities.Cyclist>
        {
            CyclistBuilder.Default().WithGroup(group).Build(),
        };

        var simulation = new Domain.Simulation.Entities.Simulation(raceDistanceKm, cyclists, new List<RaceGroup> { group });

        // Act
        var neededTicks = raceDistanceKm * 1000 / speed;
        for (int i = 0; i < neededTicks + 1; i++)   // +1 cuz of the deviation
            simulation.Advance();

        // Assert
        Assert.True(simulation.IsFinished);
    }
}
