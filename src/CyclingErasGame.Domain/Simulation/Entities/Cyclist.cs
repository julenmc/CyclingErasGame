using CyclingErasGame.Domain.Simulation.Enums;

namespace CyclingErasGame.Domain.Simulation.Entities;

public class Cyclist
{
    internal Guid Id { get; }
    internal RaceGroup Group { get; private set; }

    // Probablemente esto tenga que ir en un VO con actitud, esfuerzo, a quien esperar (opcional), a quién perseguir, qué rueda seguir...
    internal CyclistAttitude Attitude { get; private set; }
    internal int CurrentEffort { get; private set; }

    public Cyclist(
        Guid id,
        RaceGroup group,
        CyclistAttitude attitude = CyclistAttitude.KeepPosition,
        int currentEffort = 20)
    {
        Id = id;
        Group = group;
        Attitude = attitude;
        CurrentEffort = currentEffort;
    }

    internal void MoveToGroup(RaceGroup raceGroup) => Group = raceGroup;
}
