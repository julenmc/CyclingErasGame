namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

public class SimulationBuilder
{
    private double _raceDistance = 10;
    private List<Domain.Simulation.Entities.Cyclist> _cyclists = new();
    private List<Domain.Simulation.Entities.RaceGroup> _groups = new();

    public SimulationBuilder WithDistance(double raceDistance)
    {
        _raceDistance = raceDistance;
        return this;
    }

    public SimulationBuilder WithGroups(List<Domain.Simulation.Entities.RaceGroup> groups)
    {
        _groups = groups; 
        return this;
    }

    public SimulationBuilder WithCyclists(List<Domain.Simulation.Entities.Cyclist> cyclists)
    {
        _cyclists = cyclists;
        return this;
    }

    public Domain.Simulation.Entities.Simulation Build()
        => new Domain.Simulation.Entities.Simulation(
            _raceDistance,
            _cyclists,
            _groups);

    public static SimulationBuilder Default() => new SimulationBuilder();
}
