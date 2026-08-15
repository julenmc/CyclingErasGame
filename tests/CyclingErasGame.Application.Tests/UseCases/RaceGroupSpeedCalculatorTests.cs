using CyclingErasGame.Application.UseCases;
using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;
using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Tests.Cyclist;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;
using Moq;

using CyclistEntity = CyclingErasGame.Domain.Cyclist.Entities.Cyclist;
using SimulationCyclist = CyclingErasGame.Domain.Simulation.Entities.Cyclist;

namespace CyclingErasGame.Application.Tests.UseCases;

public class RaceGroupSpeedCalculatorTests
{
    Mock<ISpeedProvider> speedProviderMock;
    Mock<ICyclistPowerCalculatorService> cyclistPowerCalculatorServiceMock;
    RaceGroupSpeedCalculationUseCase calculator;

    List<CyclistEntity> cyclists = new();
    List<SimulationCyclist> simulationCyclists = new();
    List<RaceGroup> groups = new();
    Simulation? simulation;

    public RaceGroupSpeedCalculatorTests()
    {
        speedProviderMock = new Mock<ISpeedProvider>();
        cyclistPowerCalculatorServiceMock = new Mock<ICyclistPowerCalculatorService>();
        calculator = new RaceGroupSpeedCalculationUseCase(
            speedProviderMock.Object,
            cyclistPowerCalculatorServiceMock.Object);
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

    #region SpeedProvider
    [Theory]
    [InlineData(0)]
    [InlineData(200)]
    [InlineData(400)]
    public void Calculate_WithSingleGroupWithOneCyclist_WithDifferentPowers_GivesSpeedProviderCorrectPower(double power)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>
        {
            { 1, 1 }
        };
        BuildSimulation(groupConfigurations);

        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(It.IsAny<CyclistEntity>(), It.IsAny<SimulationCyclist>()))
                                         .Returns(power);

        // Act
        var result = calculator.Calculate(simulation!, cyclists);

        // Assert
        speedProviderMock.Verify(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), power), Times.Once);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    public void Calculate_WithSingleGroupWithMultipleCyclist_WithDifferentPowers_GivesSpeedProviderCorrectPower(int cyclistCount)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>
        {
            { 1, cyclistCount }
        };
        BuildSimulation(groupConfigurations);

        int powerCounter = 0;
        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(It.IsAny<CyclistEntity>(), It.IsAny<SimulationCyclist>()))
                                         .Returns(() => powerCounter)
                                         .Callback(() => powerCounter += 10);

        // Act
        calculator.Calculate(simulation!, cyclists);

        // Assert
        for (int i = 0; i < cyclistCount; i++)
            speedProviderMock.Verify(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), i * 10), Times.Once);        
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void Calculate_WithMultipleGroupsWithSingleCyclist_WithDifferentPowers_GivesSpeedProviderCorrectPower(int groupCount)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>();
        for (int i = 0; i < groupCount; i++)
            groupConfigurations.Add(i + 1, 1);
        BuildSimulation(groupConfigurations);

        int powerCounter = 0;
        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(It.IsAny<CyclistEntity>(), It.IsAny<SimulationCyclist>()))
                                         .Returns(() => powerCounter)
                                         .Callback(() => powerCounter += 10);

        // Act
        calculator.Calculate(simulation!, cyclists);

        // Assert
        for (int i = 0; i < groupCount; i++)
            speedProviderMock.Verify(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), i * 10), Times.Once);
    }

    [Theory]
    [InlineData(2, 4)]
    [InlineData(5, 2)]
    [InlineData(10, 8)]
    public void Calculate_WithMultipleGroupsWithMultipleCyclists_WithDifferentPowers_GivesSpeedProviderCorrectPower(int groupCount, int cyclistCount)
    {
        // Arrange
        var groupConfigurations = new Dictionary<int, int>();
        for (int i = 0; i < groupCount; i++)
            groupConfigurations.Add(i + 1, cyclistCount);
        BuildSimulation(groupConfigurations);

        int powerCounter = 0;
        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(It.IsAny<CyclistEntity>(), It.IsAny<SimulationCyclist>()))
                                         .Returns(() => powerCounter)
                                         .Callback(() => powerCounter += 10);

        // Act
        calculator.Calculate(simulation!, cyclists);

        // Assert
        for (int i = 0; i < groupCount * cyclistCount; i++)
            speedProviderMock.Verify(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), i * 10), Times.Once);
    }
    #endregion

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

        speedProviderMock.Setup(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), It.IsAny<double>()))
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

        speedProviderMock.Setup(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), It.IsAny<double>()))
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

        double speedCounter = 0;
        speedProviderMock.Setup(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), It.IsAny<double>()))
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
        // The cyclist resting has a theorical speed of 50kph, the cyclist pushing has a 48kph theorical speed
        // The diff is not enough, so the "slow" one keeps pushing

        // Arrange
        double fastSpeed = 50;
        double slowSpeed = 48;
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

        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(fastCyclist, It.IsAny<SimulationCyclist>()))
                                         .Returns(200);
        speedProviderMock.Setup(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), 200))
                         .Returns(() => fastSpeed);

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

        cyclistPowerCalculatorServiceMock.Setup(m => m.Calculate(slowCyclist, It.IsAny<SimulationCyclist>()))
                                         .Returns(190);
        speedProviderMock.Setup(m => m.GetSpeed(It.IsAny<RaceConditionContext>(), 190))
                         .Returns(() => slowSpeed);

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
    #endregion
}
