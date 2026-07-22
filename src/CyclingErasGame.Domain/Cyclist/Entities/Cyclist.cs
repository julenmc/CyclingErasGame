namespace CyclingErasGame.Domain.Cyclist.Entities;

public class Cyclist
{
    public Guid Id { get; }

    public Cyclist(
        Guid id) 
    { 
        Id = id;
    }
}
