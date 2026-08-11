namespace CyclingErasGame.Domain.Common.ValueObjects;

public record SimulationContext(
    IReadOnlyList<GroupContext> GroupContexts);

public record GroupContext(
    double PointSlope);