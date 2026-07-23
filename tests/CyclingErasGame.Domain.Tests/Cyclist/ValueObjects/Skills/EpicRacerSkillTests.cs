using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;
using CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;
using static CyclingErasGame.Domain.Cyclist.ValueObjects.Skills.Skill;

namespace CyclingErasGame.Domain.Tests.Cyclist.ValueObjects.Skills;

public class EpicRacerSkillTests
{
    public static IEnumerable<object[]> Scenarios =>
        new List<object[]>
        {
            // No climber
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.None,
                SkillLevel.Low,
                0.00
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.None,
                SkillLevel.Medium,
                0.00
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.None,
                SkillLevel.High,
                0.01
            },

            // Low level climber
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Low,
                SkillLevel.Low,
                0.01
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Low,
                SkillLevel.Medium,
                0.02
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Low,
                SkillLevel.High,
                0.03
            },

            // Medium level climber
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Medium,
                SkillLevel.Low,
                0.02
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Medium,
                SkillLevel.Medium,
                0.04
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.Medium,
                SkillLevel.High,
                0.06
            },

            // High level climber
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.High,
                SkillLevel.Low,
                0.04
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.High,
                SkillLevel.Medium,
                0.07
            },
            new object[]
            {
                new AttackContext(AttackContext.Terrains.Mountain),
                RacerType.RacerTypeLevel.High,
                SkillLevel.High,
                0.1
            },
        };
    [Theory]
    [MemberData(nameof(Scenarios))]
    public void GetAttackProbability_ForClimber_ReturnsExpected(
        AttackContext context,
        RacerType.RacerTypeLevel racerLevel,
        SkillLevel skillLevel,
        double expectedProbability)
    {
        TestClimberSkill(context, skillLevel, racerLevel, expectedProbability);
    }

    private void TestClimberSkill(
        AttackContext context,
        SkillLevel skillLevel,
        RacerType.RacerTypeLevel racerlevel,
        double expectedProbability)
    {
        var cyclist = CyclistBuilder.Default()
                                    .WithType(new ClimberRacer(racerlevel))
                                    .WithSkill(new EpicRacerSkill(skillLevel))
                                    .Build();

        TestSkill(context, cyclist, expectedProbability);
    }

    private void TestSkill(
        AttackContext context, 
        Domain.Cyclist.Entities.Cyclist cyclist,
        double expectedProbability)
    {
        var skill = cyclist.Skills.OfType<EpicRacerSkill>().First();
        var probability = skill.GetAttackProbability(cyclist, context);

        Assert.Equal(expectedProbability, probability);
    }
}
