using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.Entities.Skills;

public class EpicRacerSkill : Skill
{
    public EpicRacerSkill(Guid id, SkillLevel level) : base(id, level)
    {
    }

    internal override long GetAttackProbability(Cyclist cyclist, AttackContext context)
    {
        throw new NotImplementedException();
    }
}
