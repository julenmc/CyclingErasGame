using CyclingErasGame.Domain.Cyclist.Entities.RacerTypes;
using CyclingErasGame.Domain.Cyclist.Entities.Skills;
using CyclingErasGame.Domain.Cyclist.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.Entities;

public class Cyclist
{
    public Guid Id { get; }

    public Measures Measures { get; }

    public Powers Powers { get; }

    private readonly List<Skill> _skills = new();
    public IReadOnlyList<Skill> Skills => _skills;

    private readonly List<RacerType> _racerTypes = new();
    public IReadOnlyList<RacerType> RacerTypes => _racerTypes;

    public Cyclist(
        Guid id,
        Measures measures,
        Powers powers) 
    { 
        Id = id;
        Measures = measures;
        Powers = powers;
    }

    public void AddSkill(Skill skill) => _skills.Add(skill);

    public void AddRacerType(RacerType racerType) => _racerTypes.Add(racerType);
}
