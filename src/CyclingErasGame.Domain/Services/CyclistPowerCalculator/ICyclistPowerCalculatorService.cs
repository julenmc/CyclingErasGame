namespace CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;

public interface ICyclistPowerCalculatorService
{
    double Calculate(Cyclist.Entities.Cyclist cyclist, Simulation.Entities.Cyclist simulationInfo);
}
