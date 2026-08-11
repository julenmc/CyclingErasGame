using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Adapters.CyclingPhysics;

public class PowerProvider : IPowerProvider
{
    public double GetRequiredPower(RaceConditionContext context, double speedMps)
        => Katebarik.CyclingPhysics.CyclingPhysicsCalculator.CalculateRequiredPower(Utils.GetConditions(context), speedMps);
}
