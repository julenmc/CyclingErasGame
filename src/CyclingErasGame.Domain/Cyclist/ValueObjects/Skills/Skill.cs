using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;

public abstract record Skill
{
    public SkillLevel Level { get; }

    public enum SkillLevel
    {
        None,
        Low,
        Medium,
        High
    }

    protected Skill(SkillLevel level)
    {
        Level = level;
    }

    internal abstract long GetAttackProbability(
        Entities.Cyclist cyclist,
        AttackContext context);
}
