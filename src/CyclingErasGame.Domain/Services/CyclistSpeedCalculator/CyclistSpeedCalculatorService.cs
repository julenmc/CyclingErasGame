using CyclingErasGame.Domain.Common.Interfaces.Physics;
using CyclingErasGame.Domain.Common.ValueObjects;
using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;

namespace CyclingErasGame.Domain.Services.CyclistSpeedCalculator;

public class CyclistSpeedCalculatorService : ICyclistSpeedCalculatorService
{
    private readonly ISpeedProvider _speedProvider;
    private readonly ICyclistPowerCalculatorService _powerCalculatorService;

    public CyclistSpeedCalculatorService(
        ISpeedProvider speedProvider,
        ICyclistPowerCalculatorService cyclistPowerCalculator)
    {
        _speedProvider = speedProvider;
        _powerCalculatorService = cyclistPowerCalculator;
    }

    public double Calculate(RaceConditionContext context, Cyclist.Entities.Cyclist cyclist, Simulation.Entities.Cyclist simulationInfo)
    {
        var power = _powerCalculatorService.Calculate(cyclist, simulationInfo);
        return _speedProvider.GetSpeed(context, power);
    }
}
