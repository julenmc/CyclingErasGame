namespace CyclingErasGame.Domain.Simulation.Entities;

public class Cyclist
{
    internal RaceGroup Group { get; private set; }

    public Cyclist(RaceGroup group)
    {
        Group = group;
    }

    internal void MoveToGroup(RaceGroup raceGroup) => Group = raceGroup;
}
