using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Common.Interfaces.Physics;

public interface ISpeedProvider
{
    double GetSpeed(RaceConditionContext context, double power);
}
