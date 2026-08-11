using CyclingErasGame.Domain.Simulation.Constants;

namespace CyclingErasGame.Domain.Simulation.Entities;

public class RaceGroup
{
    internal int Id { get; }
    internal double CurrentDistanceKm { get; private set; } = 0;

    private double _speedMps;   // Speed in m/s

    public RaceGroup(
        int id,
        double speedMps = 0)
    {
        Id = id;
        _speedMps = speedMps;
    }

    internal void Advance()
    {
        CurrentDistanceKm += (_speedMps * SimulationConstants.AdvancedTimeForTick) / 1000;
    }

    internal void SetSpeed(double speedMps) => _speedMps = speedMps;
}
