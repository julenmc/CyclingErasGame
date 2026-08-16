using CyclingErasGame.Domain.Simulation.Constants;

namespace CyclingErasGame.Domain.Simulation.Entities;

public class RaceGroup
{
    public int Id { get; }
    public double CurrentDistanceKm { get; private set; } = 0;

    public double SpeedMps { get; private set; }   // Speed in m/s

    private Queue<Cyclist> _relayingCyclists = new();

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

    internal void AddCyclistToRelays(Cyclist cyclist)
    {
        _relayingCyclists.Enqueue(cyclist);
    }

    internal void RemoveCyclistFromRelays(Cyclist cyclist)
    {
        int count = _relayingCyclists.Count;

        for (int i = 0; i < count; i++)
        {
            var current = _relayingCyclists.Dequeue();
            if (!EqualityComparer<Cyclist>.Default.Equals(current, cyclist))
                _relayingCyclists.Enqueue(current);
        }
    }

    public Cyclist? GetFirstCyclist() => _relayingCyclists.FirstOrDefault();
}
