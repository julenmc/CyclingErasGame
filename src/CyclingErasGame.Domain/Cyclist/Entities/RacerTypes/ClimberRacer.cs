namespace CyclingErasGame.Domain.Cyclist.Entities.RacerTypes;

public record ClimberRacer : RacerType
{
    public ClimberRacer(RacerTypeLevel level) : base(level)
    {
    }
}
