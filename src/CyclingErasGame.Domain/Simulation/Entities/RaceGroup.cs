using CyclingErasGame.Domain.Simulation.Constants;

namespace CyclingErasGame.Domain.Simulation.Entities;

public class RaceGroup
{
    public int Id { get; }
    public double CurrentDistanceKm { get; private set; } = 0;

    public double SpeedMps { get; private set; }   // Speed in m/s

    public RaceGroup(
        int id,
        double speedMps = 0)
    {
        Id = id;
        SpeedMps = speedMps;
    }

    internal void Advance()
    {
        CurrentDistanceKm += (SpeedMps * SimulationConstants.AdvancedTimeForTick) / 1000;
    }

    internal void SetSpeed(double speedMps) => SpeedMps = speedMps;
}
