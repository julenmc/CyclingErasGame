using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Simulation.Entities;

public class SimulationTests
{
    #region Advance
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
                                    .WithSpeed(speed)
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
    #endregion // Advance

    #region UpdateGroupSpeeds
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    public void UpdateGroupSpeeds_WithSingleGroup_UpdatesSpeed(int speed)
    {
        // Arrange
        var group = RaceGroupBuilder.Default()
                                    .Build();
        var cyclists = new List<Domain.Simulation.Entities.Cyclist>
        {
            CyclistBuilder.Default().WithGroup(group).Build(),
        };

        var simulation = new Domain.Simulation.Entities.Simulation(10, cyclists, new List<RaceGroup> { group });

        // Act
        Assert.NotEqual(speed, simulation.Groups[0].SpeedMps);
        simulation.UpdateGroupSpeeds(
            new Dictionary<int, double>
            {
                { 0, speed }
            });

        // Assert
        Assert.Equal(speed, simulation.Groups[0].SpeedMps);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void UpdateGroupSpeeds_WithMultipleGroups_UpdatesSpeeds(int groupCount)
    {
        // Arrange
        var groupList = new List<RaceGroup>();
        var cyclistList = new List<Domain.Simulation.Entities.Cyclist>();
        for (int i = 0; i < groupCount; i++)
        {
            var group = RaceGroupBuilder.Default()
                                        .WithId(i)
                                        .Build();
            groupList.Add(group);
            cyclistList.Add(CyclistBuilder.Default().WithGroup(group).Build());
        }

        var simulation = new Domain.Simulation.Entities.Simulation(10, cyclistList, groupList);

        // Act. Group speed will be its id + 1
        foreach (var group in simulation.Groups)
            Assert.NotEqual(group.Id + 1, group.SpeedMps);

        var speeds = new Dictionary<int, double>();
        foreach (var group in simulation.Groups)
            speeds.Add(group.Id, group.Id + 1);

        simulation.UpdateGroupSpeeds(speeds);

        // Assert
        foreach (var group in simulation.Groups)
            Assert.Equal(group.Id + 1, group.SpeedMps);
    }
    #endregion // UpdateGroupSpeeds
}
