using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

internal class CyclistBuilder
{
    private RaceGroup _group = RaceGroupBuilder.Default().Build();

    internal CyclistBuilder WithGroup(RaceGroup group)
    {
        _group = group;
        return this;
    }

    internal Domain.Simulation.Entities.Cyclist Build()
    {
        var cyclist = new Domain.Simulation.Entities.Cyclist(_group);

        return cyclist;
    }

    internal static CyclistBuilder Default() => new CyclistBuilder();
}
