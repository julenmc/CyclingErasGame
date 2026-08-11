namespace CyclingErasGame.Domain.Common.ValueObjects;

public record RaceConditionContext
{
    public enum DraftPosition
    {
        Front,
        BehindRider,
        InGroup
    };

    public required double RiderMassKg { get; init; }

    public double BikeMassKg { get; init; } = 7.0;

    /// Pendiente en porcentaje.
    public required double GradientPercent { get; init; }

    /// Coeficiente de rodadura.
    public double Crr { get; init; } = 0.003;

    /// CdA del ciclista.
    public double Cda { get; init; } = 0.32;

    /// Viento en m/s.
    /// Positivo = viento de cara.
    /// Negativo = viento de cola.
    public double WindSpeed { get; init; } = 0;

    public DraftPosition Drafting { get; init; } = DraftPosition.Front;
}
