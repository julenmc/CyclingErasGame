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
        var result = new Dictionary<int, double>();

        foreach (var group in simulation.Groups)
        {
            var cyclistsInfos = simulation.Cyclists
                .Where(c => c.Group.Id == group.Id)
                .ToList();

            var ids = new HashSet<Guid>(cyclistsInfos.Select(b => b.Id));
            var cyclistsInGroup = cyclists
                .Where(c => ids.Contains(c.Id))
                .ToList();

            // add group speed to dicctionary
            result.Add(group.Id, CalculateGroupSpeed(group, cyclistsInGroup, cyclistsInfos));
        }

        return result;
    }

    private double CalculateGroupSpeed(
        RaceGroup group,
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
        var speeds = new Dictionary<Guid, double>();
        foreach (var cyclist in cyclistsInGroup)
        {
            var contextForCyclist = baseContext with
            {
                RiderMassKg = cyclist.Measures.WeightKg
            };
            var power = _cyclistPowerCalculator.Calculate(cyclist, cyclistsSimInfo.First(c => c.Id == cyclist.Id));
            speeds.Add(cyclist.Id, _speedProvider.GetSpeed(contextForCyclist, power));
        }

        // Relays continue
        var firstCyclist = group.GetFirstCyclist();

        return speeds[firstCyclist.Id];
    }
}
