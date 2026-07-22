using CyclingErasGame.Domain.Cyclist.ValueObjects;

namespace CyclingErasGame.Domain.Cyclist.Entities;

public class Cyclist
{
    public Guid Id { get; }

    public Measures Measures { get; }

    public Powers Powers { get; }

    public Cyclist(
        Guid id,
        Measures measures,
        Powers powers) 
    { 
        Id = id;
        Measures = measures;
        Powers = powers;
    }
}
