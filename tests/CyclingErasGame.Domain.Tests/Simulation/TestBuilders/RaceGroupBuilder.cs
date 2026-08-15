using CyclingErasGame.Domain.Simulation.Entities;

namespace CyclingErasGame.Domain.Tests.Simulation.TestBuilders;

public class RaceGroupBuilder
{
    private int _id = 0;
    private double _speedMps = 0;

    public RaceGroupBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public RaceGroupBuilder WithSpeed(double speedMps)
    {
        _speedMps = speedMps;
        return this;
    }

    public RaceGroup Build()
    {
        return new RaceGroup(
            _id,
            _speedMps);
    }

    public static RaceGroupBuilder Default() => new RaceGroupBuilder();
}
