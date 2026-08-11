using CyclingErasGame.Domain.Cyclist.ValueObjects;
using CyclingErasGame.Domain.Cyclist.ValueObjects.Powers;
using CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;
using CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;

namespace CyclingErasGame.Domain.Tests.Cyclist;

internal class CyclistBuilder
{
    private Measures _measures = new Measures(180, 70);
    private CyclistPowerValues _powers = new CyclistPowerValues(50, 50, 50);
    private List<RacerType> _types = new();
    private List<Skill> _skills = new();

    internal CyclistBuilder WithMeasures(Measures measures)
    {
        _measures = measures;
        return this;
    }

    internal CyclistBuilder WithPowers(CyclistPowerValues powers)
    {
        _powers = powers;
        return this;
    }

    internal CyclistBuilder WithType(RacerType type)
    {
        _types.Add(type);
        return this;
    }

    internal CyclistBuilder WithSkill(Skill skill)
    {
        _skills.Add(skill);
        return this;
    }

    internal Domain.Cyclist.Entities.Cyclist Build()
    {
        var cyclist = new Domain.Cyclist.Entities.Cyclist(
            Guid.NewGuid(),
            _measures,
            _powers);

        foreach (var type in _types)
            cyclist.AddRacerType(type);

        foreach (var skill in _skills)
            cyclist.AddSkill(skill);

        return cyclist;
    }

    internal static CyclistBuilder Default() => new CyclistBuilder();
}
