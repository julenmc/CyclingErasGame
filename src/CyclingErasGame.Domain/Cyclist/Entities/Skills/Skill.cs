namespace CyclingErasGame.Domain.Cyclist.Entities.Skills;

public abstract class Skill
{
    public Guid Id { get; }
    public SkillLevel Level { get; }

    protected Skill(
        Guid id, 
        SkillLevel level)
    {
        Id = id; 
        Level = level;
    }

    public enum SkillLevel
    {
        None,
        Low,
        Medium,
        High
    }
}
