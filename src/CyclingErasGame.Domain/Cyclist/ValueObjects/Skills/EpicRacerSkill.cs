using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;

public record EpicRacerSkill : Skill
{
    public EpicRacerSkill(SkillLevel level) : base(level)
    {
    }

    internal override long GetAttackProbability(Entities.Cyclist cyclist, AttackContext context)
    {
        throw new NotImplementedException();
    }
}
