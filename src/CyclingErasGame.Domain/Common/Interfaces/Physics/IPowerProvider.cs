using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Common.Interfaces.Physics;

public interface IPowerProvider
{
    double GetRequiredPower(RaceConditionContext context, double speedMps);
}
