using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

internal class RaceGroupBuilder
{
    private int _id = 0;
    private double _speedMps = 0;

    internal RaceGroupBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    internal RaceGroupBuilder WithSpeed(double speedMps)
    {
        _speedMps = speedMps;
        return this;
    }

    internal RaceGroup Build()
    {
        return new RaceGroup(
            _id,
            _speedMps);
    }

    internal static RaceGroupBuilder Default() => new RaceGroupBuilder();
}
