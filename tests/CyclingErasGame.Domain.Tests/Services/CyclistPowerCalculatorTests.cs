using CyclingErasGame.Domain.Services;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Services;

public class CyclistPowerCalculatorTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(25, 133)]
    [InlineData(50, 267)]
    [InlineData(75, 400)]
    [InlineData(80, 500)]
    [InlineData(85, 600)]
    [InlineData(90, 773)]
    [InlineData(95, 1090)]
    [InlineData(100, 1500)]
    public void Calculate_ReturnsCorrectValue(int effort, double expectedPower)
    {
        // Arrange
        var cyclist = Cyclist.CyclistBuilder.Default()
                                            .WithPowers(new Domain.Cyclist.ValueObjects.Powers.CyclistPowerValues(1500, 600, 400))
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
