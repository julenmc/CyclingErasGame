using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.Entities.Skills;

public abstract class Skill
{
    public Guid Id { get; }
    public SkillLevel Level { get; }

    public enum SkillLevel
    {
        None,
        Low,
        Medium,
        High
    }

    protected Skill(
    Guid id,
    SkillLevel level)
    {
        Id = id;
        Level = level;
    }

    internal abstract long GetAttackProbability(
        Cyclist cyclist,
        AttackContext context);
}
