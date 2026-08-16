using CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;
using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Application.UseCases.SimulationFlow;

internal class SimulationTick
{
    private readonly IRaceGroupSpeedCalculation _groupSpeedCalculator;
    private readonly Simulation _simulationInfo;
    private readonly IReadOnlyList<Domain.Cyclist.Entities.Cyclist> _cyclists;

    internal SimulationTick(
        IRaceGroupSpeedCalculation groupSpeedCalculator,
        Simulation simulationInfo,
        IReadOnlyList<Domain.Cyclist.Entities.Cyclist> cyclists)
    {
        _groupSpeedCalculator = groupSpeedCalculator;
        _simulationInfo = simulationInfo;
        _cyclists = cyclists;
    }

    internal void Tick()
    {
        // 1. Consume events

        // 2. Advance
        Advance();

        // 3. Update sim status
    }

    private void Advance()
    {
        // 1. Group speeds
        var groupSpeeds = _groupSpeedCalculator.Calculate(_simulationInfo, _cyclists);
    }
}
