namespace CyclingErasGame.Domain.Cyclist.ValueObjects.RacerTypes;

public abstract record RacerType
{
    public enum RacerTypeLevel
    {
        None,
        Low,
        Medium, 
        High
    }

    public RacerTypeLevel Level { get; }

    protected RacerType(RacerTypeLevel level)
    {
        Level = level;
    }
}
