namespace CyclingErasGame.Domain.Common.Constants;

internal static class CommonConstants
{
    internal const double ShortIntervalSeconds = 10;        // 10 seconds (below this it's 100% short interval power)
    internal const double MediumIntervalSeconds = 5 * 60;   // 5 minutes (below this it's mixed with short power, above with long power)
    internal const double LongIntervalSeconds = 20 * 60;    // 20 minutes (above this it's 100% long interval power)
}
