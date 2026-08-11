using CyclingErasGame.Domain.Cyclist.ValueObjects.Skills;
using CyclingErasGame.Domain.Services;
using static CyclingErasGame.Domain.Cyclist.ValueObjects.Skills.Skill;

namespace CyclingErasGame.Domain.Tests.Services;

public class CyclistAttitudeServiceTests
{
    [Theory]
    [InlineData(SkillLevel.Low, 0.02)]
    [InlineData(SkillLevel.Medium, 0.03)]
    [InlineData(SkillLevel.High, 0.05)]
    public void GetAttackProbability_WithEpicRacerSkill_ReturnsExpected(SkillLevel lvl, double prob)
    {
        var service = new CyclistAttitudeService();
        var cyclist = new Domain.Cyclist.Entities.Cyclist(
            Guid.NewGuid(), 
            new Domain.Cyclist.ValueObjects.Measures(180, 70), 
            new Domain.Cyclist.ValueObjects.Powers.CyclistPowerValues(50, 50, 50));
        cyclist.AddSkill(new EpicRacerSkill(lvl));

        var result = service.GetAttackProbability(cyclist);

        Assert.Equal(prob, result);
    }
}
