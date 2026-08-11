namespace CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;

public record SprinterRacer : RacerType
{
    public SprinterRacer(RacerTypeLevel level) : base(level)
    {
    }
}
