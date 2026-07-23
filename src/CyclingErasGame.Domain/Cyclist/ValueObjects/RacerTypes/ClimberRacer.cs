namespace CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;

public record ClimberRacer : RacerType
{
    public ClimberRacer(RacerTypeLevel level) : base(level)
    {
    }
}
