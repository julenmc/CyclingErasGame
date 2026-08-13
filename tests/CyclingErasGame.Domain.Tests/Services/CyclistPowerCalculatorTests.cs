using CyclingErasGame.Domain.Services;
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
        var cyclistSimulation = CyclistBuilder.Default()
                                              .WithId(cyclist.Id)
                                              .WithEffort(effort)
                                              .Build();

        var calculator = new CyclistPowerCalculator();

        // Act
        var result = calculator.Calculate(cyclist, cyclistSimulation);

        // Assert
        Assert.Equal(expectedPower, result, 1.0);
    }
}
