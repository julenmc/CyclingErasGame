using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;
using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Application.UseCases;

internal class RaceGroupSpeedCalculationUseCase
{
    private readonly ISpeedProvider _speedProvider;
    private readonly ICyclistPowerCalculatorService _cyclistPowerCalculator;

    internal RaceGroupSpeedCalculationUseCase(
        ISpeedProvider speedProvider,
        ICyclistPowerCalculatorService cyclistPowerCalculator)
    {
        _speedProvider = speedProvider;
        _cyclistPowerCalculator = cyclistPowerCalculator;
    }

    internal IReadOnlyDictionary<int, double> Calculate(
        Simulation simulation, 
        IReadOnlyList<Domain.Cyclist.Entities.Cyclist> cyclists)
    {
        var result = new Dictionary<int, double>();

        foreach (var group in simulation.Groups)
        {
            var cyclistSimInfo = group.GetFirstCyclist();
            var cyclist = cyclists.First(c => c.Id == cyclistSimInfo.Id);

            var context = new RaceConditionContext
            {
                RiderMassKg = cyclist.Measures.WeightKg,
                GradientPercent = 0.06
            };

            var power = _cyclistPowerCalculator.Calculate(cyclist, cyclistSimInfo);
            var speed = _speedProvider.GetSpeed(context, power);

            // add group speed to dicctionary
            result.Add(group.Id, speed);
        }

        return result;
    }
}
