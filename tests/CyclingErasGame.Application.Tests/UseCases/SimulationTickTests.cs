using CyclingErasGame.Application.UseCases.RaceGroupSpeedCalculation;
using CyclingErasGame.Application.UseCases.SimulationFlow;
using CyclingErasGame.Domain.Simulation.Entities;
using Moq;

namespace CyclingErasGame.Application.Tests.UseCases;

public class SimulationTickTests
{
    Mock<IRaceGroupSpeedCalculation> _groupSpeedCalculatorMock;
    SimulationTick simulator;

    Simulation simulation;
    List<Domain.Cyclist.Entities.Cyclist> cyclists;

    public SimulationTickTests()
    {
        _groupSpeedCalculatorMock = new Mock<IRaceGroupSpeedCalculation>();
    }

    private void SetUp()
    {
        simulation = new Simulation(20, new List<Cyclist>(), new List<RaceGroup>());
        simulator = new SimulationTick(_groupSpeedCalculatorMock.Object, simulation, new List<Domain.Cyclist.Entities.Cyclist>());
    }

    [Fact]
    public void Tick_CallsGroupSpeedCalculator()
    {
        // Assert 
        SetUp();

        // Act
        simulator.Tick();

        // Assert
        _groupSpeedCalculatorMock.Verify(m => m.Calculate(It.IsAny<Simulation>(), It.IsAny<IReadOnlyList<Domain.Cyclist.Entities.Cyclist>>()), Times.Once);
    }
}
