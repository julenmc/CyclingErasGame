using CyclingErasGame.Domain.Simulation.Entities;
using CyclingErasGame.Domain.Simulation.Enums;

namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

internal class CyclistBuilder
{
    private Guid _id = new Guid();
    private RaceGroup _group = RaceGroupBuilder.Default().Build();
    private CyclistAttitude _attitude = CyclistAttitude.KeepPosition;
    private int _effort = 20;

    internal CyclistBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    internal CyclistBuilder WithGroup(RaceGroup group)
    {
        _group = group;
        return this;
    }

    internal CyclistBuilder WithAttitude(CyclistAttitude attitude)
    {
        _attitude = attitude;
        return this;
    }

    internal CyclistBuilder WithEffort(int effort)
    {
        _effort = effort;
        return this;
    }

    internal Domain.Simulation.Entities.Cyclist Build()
    {
        var cyclist = new Domain.Simulation.Entities.Cyclist(
            _id,
            _group,
            _attitude,
            _effort);

        return cyclist;
    }

    internal static CyclistBuilder Default() => new CyclistBuilder();
}
