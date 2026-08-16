using CyclingErasGame.Domain.Services.CyclistPowerCalculator;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Services;

public class CyclistPowerCalculatorTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(15, 63)]    // Z1
    [InlineData(30, 125)]   // Z2
    [InlineData(50, 208)]   // Z3
    [InlineData(60, 250)]   // Z4
    [InlineData(70, 300)]   // Z4-5
    [InlineData(80, 350)]   // Z5
    [InlineData(85, 419)]   // Z6
    [InlineData(100, 900)]  // Z7
    public void Calculate_ReturnsCorrectValue(int effort, double expectedPower)
    {
        // Arrange
        var cyclist = Cyclist.CyclistBuilder.Default()
                                            .WithPowers(new Domain.Cyclist.ValueObjects.Powers.CyclistPowerValues(900, 350, 250))
                                            .Build();
        var cyclistSimulation = SimulationCyclistBuilder.Default()
                                              .WithId(cyclist.Id)
                                              .WithEffort(effort)
                                              .WithAttitude(Domain.Simulation.Enums.CyclistAttitude.SetRhythm)
                                              .Build();

        var calculator = new CyclistPowerCalculatorService();

        // Act
        var result = calculator.Calculate(cyclist, cyclistSimulation);

        // Assert
        Assert.Equal(expectedPower, result, 1.0);
    }

    [Fact]
    public void Calculate_WithCyclistNotPushing_ReturnsDefaultPower()
    {
        // Arrange
        var cyclist = Cyclist.CyclistBuilder.Default()
                                            .WithPowers(new Domain.Cyclist.ValueObjects.Powers.CyclistPowerValues(900, 350, 250))
                                            .Build();
        var cyclistSimulation = SimulationCyclistBuilder.Default()
                                              .WithId(cyclist.Id)
                                              .WithEffort(50)
                                              .WithAttitude(Domain.Simulation.Enums.CyclistAttitude.KeepPosition)
                                              .Build();

        var calculator = new CyclistPowerCalculatorService();

        // Act
        var result = calculator.Calculate(cyclist, cyclistSimulation);

        // Assert
        Assert.Equal(100, result);
    }
}
