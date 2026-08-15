using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Cyclist.Entities;
using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;
using CyclingErasGame.Domain.Simulation.Entities;
using System.Collections.Generic;

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
        // check attack

        // get cyclist to relay. Empty??


        // get relay cyclists' speed
        var cyclistsInfos = simulation.Cyclists
            .Where(c => c.Group.Id == 1)
            .ToList();

        var ids = new HashSet<Guid>(cyclistsInfos.Select(b => b.Id));
        var cyclistsInGroup = cyclists
            .Where(c => ids.Contains(c.Id))
            .ToList();

        var speed = CalculateGroupSpeed(cyclistsInGroup, cyclistsInfos);

        // check if relay stack has to be broken (new max speed)

        // add group speed to dicctionary

        // return result
        return new Dictionary<int, double>
        {
            { 1, speed }
        };
    }

    private double CalculateGroupSpeed(
        IReadOnlyList<Domain.Cyclist.Entities.Cyclist> cyclistsInGroup,
        IReadOnlyList<Domain.Simulation.Entities.Cyclist> cyclistsSimInfo)
    {
        // check attack

        // get cyclist to relay. Empty??


        // get relay cyclists' speed
        var baseContext = new RaceConditionContext
        {
            RiderMassKg = 0,
            GradientPercent = 0.06
        };
        double speed = 0.0;
        foreach (var cyclist in cyclistsInGroup)
        {
            var contextForCyclist = baseContext with
            {
                RiderMassKg = cyclist.Measures.WeightKg
            };
            var power = _cyclistPowerCalculator.Calculate(cyclist, cyclistsSimInfo.First(c => c.Id == cyclist.Id));
            speed = _speedProvider.GetSpeed(contextForCyclist, power);
        }

        return speed;
    }
}
