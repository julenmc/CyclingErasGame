using CyclingErasGame.Domain.Services.CyclistPowerCalculator.CyclistPowerCalculator;

namespace CyclingErasGame.Domain.Services.CyclistPowerCalculator;

public class CyclistPowerCalculatorService : ICyclistPowerCalculatorService
{
    private const double MaxEffort = 100.0;
    private const double MediumEffortThreshold = 80.0;
    private const double LongEffortThreshold = 60.0;

    private const double LowEffort = 100;

    public double Calculate(Cyclist.Entities.Cyclist cyclist, Simulation.Entities.Cyclist simulationInfo)
    {
        if (simulationInfo.Attitude != Simulation.Enums.CyclistAttitude.Attack ||
            simulationInfo.Attitude != Simulation.Enums.CyclistAttitude.SetRhythm)
            return LowEffort;
        var effort = simulationInfo.CurrentEffort;
        var cyclistPowersFractions = GetPowerFractions(effort);

        return cyclist.Powers.ShortTime * cyclistPowersFractions[0] +
               cyclist.Powers.MediumTime * cyclistPowersFractions[1] +
               cyclist.Powers.LongTime * cyclistPowersFractions[2];
    }

    private static double[] GetPowerFractions(double effort)
    {
        double shortFraction = 0, mediumFraction = 0, longFraction = 0;

        if (effort == MaxEffort)
        {
            shortFraction = 1;
        }
        else if (effort >= MediumEffortThreshold)
        {
            double t = (effort - MediumEffortThreshold) / (MaxEffort - MediumEffortThreshold);

            // Elegir una de las curvas:
            //shortFraction = t * t;                     // cuadrática
            shortFraction = Math.Pow(t, 1.5);          // más explosiva
            //shortFraction = 1.0 / (1 + Math.Exp(-10*(t-0.5))); // logística
            //shortFraction = t * (2 - t);                 // curva convexa tipo W′

            mediumFraction = 1 - shortFraction;
        }
        else if (effort >= LongEffortThreshold)
        {
            mediumFraction = (effort - LongEffortThreshold) / (MediumEffortThreshold - LongEffortThreshold);
            longFraction = 1 - mediumFraction;
        }
        else
        {
            longFraction = effort / LongEffortThreshold;
        }

        return [shortFraction, mediumFraction, longFraction];
    }
}
