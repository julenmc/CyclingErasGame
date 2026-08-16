using CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;
using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;
using CyclingErasGame.Domain.Services.CyclistSpeedCalculator;
using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Tests.Cyclist;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;
using Moq;

using CyclistEntity = CyclingErasGame.Domain.Cyclist.Entities.Cyclist;
using SimulationCyclist = CyclingErasGame.Domain.Simulation.Entities.Cyclist;

namespace CyclingErasGame.Application.Tests.UseCases;

public class RaceGroupSpeedCalculatorTests
{
    Mock<ICyclistSpeedCalculatorService> speedCalculatorMock;
    RaceGroupSpeedCalculationUseCase calculator;

    List<CyclistEntity> cyclists = new();
    List<SimulationCyclist> simulationCyclists = new();
    List<RaceGroup> groups = new();
    Simulation? simulation;

    public RaceGroupSpeedCalculatorTests()
    {
        speedCalculatorMock = new Mock<ICyclistSpeedCalculatorService>();
        calculator = new RaceGroupSpeedCalculationUseCase(
            speedCalculatorMock.Object);
    }

    private void BuildSimulation(Dictionary<int, int> groupsConfiguration)  // Dictionary with group id + group cyclist count
    {
        foreach (var group in groupsConfiguration)
        {
            var newGroup = RaceGroupBuilder.Default()
                                           .WithId(group.Key)
                                           .Build();
            groups.Add(newGroup);

            for (int j = 0; j < group.Value; j++)
            {
                var newCyclist = CyclistBuilder.Default()
                                               .Build();
                cyclists.Add(newCyclist);
                simulationCyclists.Add(SimulationCyclistBuilder.Default()
                                                               .WithId(newCyclist.Id)
                                                               .WithGroup(newGroup)
                                                               .Build());
            }               
        }

        simulation = SimulationBuilder.Default()
                                      .WithGroups(groups)
                                      .WithCyclists(simulationCyclists)
                                      .Build();
    }

    #region GroupSpeed
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void Calculate_WithSingleGroupWithOneCyclist_ReturnsCorrectSpeed(double speed)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>
        {
            { 1, 1 }
        };
        BuildSimulation(groupConfigurations);
        groups[0].AddCyclistToRelays(simulationCyclists[0]);

        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(), 
                                                   It.IsAny<CyclistEntity>(),
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(speed);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.Keys.First());
        Assert.Equal(speed, result[1]);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(5, 10)]
    [InlineData(10, 100)]
    public void Calculate_WithSingleGroupWithMultipleCyclists_ReturnsCorrectSpeed(double speed, int cyclistCount)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>
        {
            { 1, cyclistCount }
        };
        BuildSimulation(groupConfigurations);
        groups[0].AddCyclistToRelays(simulationCyclists[0]);

        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(),
                                                   It.IsAny<CyclistEntity>(),
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(speed);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.Keys.First());
        Assert.Equal(speed, result[1]);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void Calculate_WithMultipleGroupsWithSingleCyclist_ReturnsCorrectSpeed(int groupCount)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>();
        for (int i = 0; i < groupCount; i++)
            groupConfigurations.Add(i + 1, 1);
        BuildSimulation(groupConfigurations);
        for (int i = 0; i < groupCount; i++)
            groups[i].AddCyclistToRelays(simulationCyclists[i]);

        double speedCounter = 0;
        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(),
                                                   It.IsAny<CyclistEntity>(),
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(() => speedCounter)
                           .Callback(() => speedCounter += 10);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        Assert.Equal(groupCount, result.Count);
        for (int i = 0; i < groupCount; i++)
        {
            var groupId = i + 1;
            Assert.Contains(groupId, result.Keys);
            Assert.Equal(i * 10, result[groupId]);
        }
    }

    [Fact]
    public void Calculate_WithSingleGroupWithMultipleCyclists_ReturnsRelayingCyclistsSpeed()
    {
        // Arrange
        double fastSpeed = 20;
        double slowSpeed = 18;
        var newGroup = RaceGroupBuilder.Default()
                                       .WithId(1)
                                       .Build();
        groups.Add(newGroup);

        // Create "fast" cyclist, the one that should be resting
        var fastCyclist = CyclistBuilder.Default()
                                        .Build();
        var fastCyclistSimInfo = SimulationCyclistBuilder
            .Default()
            .WithId(fastCyclist.Id)
            .WithGroup(newGroup)
            .WithEffort(50)
            .Build();
        cyclists.Add(fastCyclist);
        simulationCyclists.Add(fastCyclistSimInfo);

        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(),
                                                   fastCyclist,
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(fastSpeed);

        // Create "slow" cyclist, the one that should be relaying
        var slowCyclist = CyclistBuilder.Default()
                                        .Build();
        var slowCyclistSimInfo = SimulationCyclistBuilder
            .Default()
            .WithId(slowCyclist.Id)
            .WithGroup(newGroup)
            .WithEffort(48)
            .Build();
        cyclists.Add(slowCyclist);
        simulationCyclists.Add(slowCyclistSimInfo);

        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(),
                                                   slowCyclist,
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(slowSpeed);

        simulation = SimulationBuilder.Default()
                                      .WithGroups(groups)
                                      .WithCyclists(simulationCyclists)
                                      .Build();

        newGroup.AddCyclistToRelays(slowCyclistSimInfo);
        newGroup.AddCyclistToRelays(fastCyclistSimInfo);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        Assert.Single(result);
        Assert.Contains(1, result.Keys);
        Assert.Equal(slowSpeed, result[1]);
    }

    [Fact]
    public void Calculate_WithSingleGroupWithMultipleCyclists_AndNoOneInFront_ReturnsCorrectSpeed()
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>
        {
            { 1, 3 }
        };
        BuildSimulation(groupConfigurations);

        speedCalculatorMock.Setup(m => m.Calculate(It.IsAny<RaceConditionContext>(),
                                                   It.IsAny<CyclistEntity>(),
                                                   It.IsAny<SimulationCyclist>()))
                           .Returns(10);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        Assert.Single(result);
        Assert.Contains(1, result.Keys);
        Assert.Equal(10, result[1]);
    }
    #endregion
}
