using CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;

namespace CyclingErasGame.Domain.Services;

internal class CyclistAttitudeService
{
    internal double GetAttackProbability(Cyclist.Entities.Cyclist cyclist)
    {
        var epicSkillLvl = cyclist.Skills
            .OfType<EpicRacerSkill>()
            .FirstOrDefault()
            ?.Level ?? Skill.SkillLevel.None;

        return epicSkillLvl switch
        {
            Skill.SkillLevel.Low => 0.02,
            Skill.SkillLevel.Medium => 0.03,
            Skill.SkillLevel.High => 0.05,
            _ => 0.001
        };
    }
}
