using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Adapters.CyclingPhysics;

public class SpeedProvider : ISpeedProvider
{
    public double GetSpeed(RaceConditionContext context, double power)
        => Katebarik.CyclingPhysics.CyclingPhysicsCalculator.CalculateSpeed(Utils.GetConditions(context), power);
}
