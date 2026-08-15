namespace CyclingErasGame.Domain.Simulation.Entities;

public class Simulation
{
    public bool IsFinished { get; private set; }

    public List<RaceGroup> Groups { get; private set; }
    public IReadOnlyList<Cyclist> Cyclists => _cyclists;

    private double _raceDistanceKm;
    private IReadOnlyList<Cyclist> _cyclists;

    public Simulation(
        double raceDistanceKm,
        IReadOnlyList<Cyclist> cyclists,
        IReadOnlyList<RaceGroup> groups)
    {
        _raceDistanceKm = raceDistanceKm;
        _cyclists = cyclists;
        Groups = groups.ToList();

        foreach (var cyclist in _cyclists)
            if (cyclist.Group == null)
                cyclist.MoveToGroup(Groups[0]);
    }

    public void Advance()
    {
        UpdateDistances();
    }

    public void UpdateGroupSpeeds(IReadOnlyDictionary<int, double> groupSpeeds)
    {
        foreach (var kvp in groupSpeeds)
        {
            var group = Groups.FirstOrDefault(g => g.Id == kvp.Key);
            group?.SetSpeed(kvp.Value);
        }
    }

    private void UpdateDistances()
    {
        foreach (var group in Groups)
        {
            if (group.CurrentDistanceKm < _raceDistanceKm)
                group.Advance();
        }

        IsFinished = Groups.All(g => g.CurrentDistanceKm >= _raceDistanceKm);
    }
}
