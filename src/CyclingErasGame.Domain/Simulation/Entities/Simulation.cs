namespace CyclingErasGame.Domain.Simulation.Entities;

public class Simulation
{
    public bool IsFinished { get; private set; }

    private double _raceDistanceKm;
    private IReadOnlyList<Cyclist> _cyclists;
    private List<RaceGroup> _groups;

    public Simulation(
        double raceDistanceKm,
        IReadOnlyList<Cyclist> cyclists,
        IReadOnlyList<RaceGroup> groups)
    {
        _raceDistanceKm = raceDistanceKm;
        _cyclists = cyclists;
        _groups = groups.ToList();

        foreach (var cyclist in _cyclists)
            cyclist.MoveToGroup(_groups[0]);
    }

    public void Advance()
    {
        UpdateDistances();
    }

    private void UpdateDistances()
    {
        foreach (var group in _groups)
        {
            if (group.CurrentDistanceKm < _raceDistanceKm)
                group.Advance();
        }

        IsFinished = _groups.All(g => g.CurrentDistanceKm >= _raceDistanceKm);
    }

    private void UpdateGroupSpeeds()
    {
        foreach (var group in _groups)
            group.SetSpeed(10);
    }
}
