using CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;
using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Application.UseCases.SimulationFlow;

public class SimulationFlowUseCase : ISimulationFlowUseCase
{
    private readonly SimulationTick _simulationTick;
    private readonly Simulation _simulationInfo;

    public SimulationFlowUseCase(IRaceGroupSpeedCalculation groupSpeedCalculator)
    {
        _simulationInfo = new Simulation(20, new List<Cyclist>(), new List<RaceGroup>());
        _simulationTick = new SimulationTick(groupSpeedCalculator, _simulationInfo, new List<Domain.Cyclist.Entities.Cyclist>());
    }

    public void Simulate()
    {
        while (!_simulationInfo.IsFinished)
        {
            _simulationTick.Tick();
        }
    }
}
