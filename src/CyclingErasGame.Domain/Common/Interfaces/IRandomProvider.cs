namespace CyclingErasGame.Domain.Common.Interfaces;

public interface IRandomProvider
{
    /// <summary>
    /// Method to check if an action with a given probability has to be made
    /// </summary>
    /// <param name="probability">Probability to make the action</param>
    /// <returns>True when action has to be made. False when not.</returns>
    bool DoAction(double probability);
}
