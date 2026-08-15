using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Simulation.Enums;

namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

public class SimulationCyclistBuilder
{
    private Guid _id = new Guid();
    private RaceGroup _group = RaceGroupBuilder.Default().Build();
    private CyclistAttitude _attitude = CyclistAttitude.KeepPosition;
    private int _effort = 20;

    public SimulationCyclistBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SimulationCyclistBuilder WithGroup(RaceGroup group)
    {
        _group = group;
        return this;
    }

    public SimulationCyclistBuilder WithAttitude(CyclistAttitude attitude)
    {
        _attitude = attitude;
        return this;
    }

    public SimulationCyclistBuilder WithEffort(int effort)
    {
        _effort = effort;
        return this;
    }

    public Domain.Simulation.Entities.Cyclist Build()
    {
        var cyclist = new Domain.Simulation.Entities.Cyclist(
            _id,
            _group,
            _attitude,
            _effort);

        return cyclist;
    }

    public static SimulationCyclistBuilder Default() => new SimulationCyclistBuilder();
}
