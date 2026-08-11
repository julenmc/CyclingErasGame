namespace CyclingErasGame.Domain.Simulation.ValueObjects;

public sealed record DraftingConditions
{
    /// Factor multiplicador del CdA.
    /// 1.0 = sin rebufo.
    /// 0.70 = reduce el CdA un 30%.
    public required double CdaFactor { get; init; }
}

public sealed record CyclingConditions
{
    public required double RiderMassKg { get; init; }

    public double BikeMassKg { get; init; } = 7.0;

    /// Pendiente en porcentaje.
    public required double GradientPercent { get; init; }

    /// Coeficiente de rodadura.
    public required double Crr { get; init; }

    /// CdA del ciclista.
    public double Cda { get; init; } = 0.32;

    /// Viento en m/s.
    /// Positivo = viento de cara.
    /// Negativo = viento de cola.
    public double WindSpeed { get; init; }

    public DraftingConditions Drafting { get; init; } = new()
    {
        CdaFactor = 1.0
    };
}
