using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

namespace CyclingErasGame.Domain.Tests.Simulation.Entities;

public class RaceGroupTests
{
    [Fact]
    public void Advance_WithNonZeroSpeed_CurrentDistanceIncreases()
    {
        // Arrange
        var group = RaceGroupBuilder.Default().Build();
        group.SetSpeed(10);

        Assert.Equal(0, group.CurrentDistanceKm);

        // Act 
        group.Advance();

        // Arrange
        Assert.Equal(0.01, group.CurrentDistanceKm);
    }
}
