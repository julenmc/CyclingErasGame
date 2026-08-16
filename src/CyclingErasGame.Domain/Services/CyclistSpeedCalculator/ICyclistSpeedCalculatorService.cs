using CyclingErasGame.Domain.Common.ValueObjects;

namespace CyclingErasGame.Domain.Services.CyclistSpeedCalculator;

public interface ICyclistSpeedCalculatorService
{
    double Calculate(RaceConditionContext context, Cyclist.Entities.Cyclist cyclist, Simulation.Entities.Cyclist simulationInfo);
}
