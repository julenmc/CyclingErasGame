using CyclingErasGame.Domain.Simulation.Constants;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Simulation.Entities;

public class RaceGroupTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5.7)]
    [InlineData(10.34)]
    [InlineData(26.7)]
    public void Advance_WithNonZeroSpeed_CurrentDistanceIncreases(double speedMps)
    {
        // Arrange
        var group = RaceGroupBuilder.Default().Build();
        group.SetSpeed(speedMps);

        Assert.Equal(0, group.CurrentDistanceKm);

        // Act 
        group.Advance();

        // Arrange
        var expectedDistance = SimulationConstants.AdvancedTimeForTick * speedMps / 1000;
        Assert.Equal(expectedDistance, group.CurrentDistanceKm);
    }
}
