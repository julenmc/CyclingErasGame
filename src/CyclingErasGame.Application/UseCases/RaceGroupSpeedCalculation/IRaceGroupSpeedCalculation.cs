using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;

public interface IRaceGroupSpeedCalculation
{
    IReadOnlyDictionary<int, double> Calculate(
        Simulation simulation,
        IReadOnlyList<Domain.Cyclist.Entities.Cyclist> cyclists);
}
