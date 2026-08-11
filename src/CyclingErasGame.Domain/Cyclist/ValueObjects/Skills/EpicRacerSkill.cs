using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;

namespace CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;

public record EpicRacerSkill : Skill
{
    public EpicRacerSkill(SkillLevel level) : base(level)
    {
    }

    internal override double GetAttackProbability(Entities.Cyclist cyclist, AttackContext context)
    {
        switch (context.Terrain)
        {
            case AttackContext.Terrains.Mountain:
                var climberLevel = cyclist.GetLevel<ClimberRacer>();
                return ClimberAttackProbabilities[(int)Level, (int)climberLevel];

            case AttackContext.Terrains.Downhill:
                // Igual que con mountain con su propio diccionario
                break;

            case AttackContext.Terrains.Flat:
                // Igual que con mountain con su propio diccionario
                break;
        }
        
        return 0;
    }

    // Give first SkillLevel, then climber level
    private static readonly double[,] ClimberAttackProbabilities =
    {
        // SkillLevel.None
        { 0.00, 0.00, 0.00, 0.00},

        // SkillLevel.Low
        { 0.00, 0.01, 0.02, 0.04 },

        // SkillLevel.Medium
        { 0.00, 0.02, 0.04, 0.07 },

        // SkillLevel.High
        { 0.01, 0.03, 0.06, 0.10 }
    };
}