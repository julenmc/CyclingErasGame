using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Adapters.CyclingPhysics;

internal static class Utils
{
    internal static Katebarik.CyclingPhysics.CyclingConditions GetConditions(RaceConditionContext source)
        => new Katebarik.CyclingPhysics.CyclingConditions
        {
            RiderMassKg = source.RiderMassKg,
            BikeMassKg = source.BikeMassKg,
            GradientPercent = source.GradientPercent,
            Crr = source.Crr,
            Cda = source.Cda,
            WindSpeed = source.WindSpeed,
            Drafting = new Katebarik.CyclingPhysics.DraftingConditions { CdaFactor = Utils.GetDraftingValue(source.Drafting) }
        };

    private static double GetDraftingValue(RaceConditionContext.DraftPosition position)
        => position switch
        {
            RaceConditionContext.DraftPosition.Front => 1.0,
            RaceConditionContext.DraftPosition.BehindRider => 0.8,
            RaceConditionContext.DraftPosition.InGroup => 0.65,
            _ => 1.0
        };
}
