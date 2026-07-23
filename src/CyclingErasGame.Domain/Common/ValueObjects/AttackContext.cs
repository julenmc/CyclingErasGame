namespace CyclingErasGame.Domain.Common.ValueObjects;

public record AttackContext
{
    internal enum Terrains
    {
        Flat,
        Mountain,
        Downhill
    }

    internal Terrains Terrain { get; }

    internal AttackContext(Terrains terrain)
    {
        Terrain = terrain;
    }
}