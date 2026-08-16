using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Services.CyclistSpeedCalculator;
using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;

public class RaceGroupSpeedCalculationUseCase : IRaceGroupSpeedCalculation
{
    private readonly ICyclistSpeedCalculatorService _speedCalculator;

    internal RaceGroupSpeedCalculationUseCase(
        ICyclistSpeedCalculatorService speedCalculator)
    {
        _speedCalculator = speedCalculator;
    }

    public IReadOnlyDictionary<int, double> Calculate(
        Simulation simulation, 
        IReadOnlyList<Domain.Cyclist.Entities.Cyclist> cyclists)
    {
        var result = new Dictionary<int, double>();

        foreach (var group in simulation.Groups)
        {
            var cyclistSimInfo = group.GetFirstCyclist();
            if (cyclistSimInfo == null)     // When no cyclist is relaying, get the first one that appears in group
                cyclistSimInfo = simulation.Cyclists.First(c => c.Group.Id == group.Id);
            var cyclist = cyclists.First(c => c.Id == cyclistSimInfo.Id);

            var context = new RaceConditionContext
            {
                RiderMassKg = cyclist.Measures.WeightKg,
                GradientPercent = 0.06
            };

            // add group speed to dicctionary
            result.Add(group.Id, _speedCalculator.Calculate(context, cyclist, cyclistSimInfo));
        }

        return result;
    }
}
