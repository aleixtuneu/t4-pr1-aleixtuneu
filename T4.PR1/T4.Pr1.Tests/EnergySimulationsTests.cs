using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using T4.PR1.Model;

namespace T4.PR1.Tests
{
    [TestClass]
    public class EnergySimulationTests
    {
        [TestMethod]
        public void CalculateEnergy_SolarSystem_ShouldReturnCorrectValue()
        {
            // Arrange
            decimal inputValue = 100;
            decimal ratio = 1.5m;
            decimal costPerKWh = 0.10m;
            decimal pricePerKWh = 0.20m;

            // Act
            var simulation = new EnergySimulation("Solar", inputValue, ratio, costPerKWh, pricePerKWh);

            // Assert
            Assert.AreEqual(inputValue * ratio, simulation.EnergyGenerated, "L'energia generada no és correcta.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidSystemType_ShouldThrowException()
        {
            // Act
            var simulation = new EnergySimulation("InvalidType", 100, 1.5m, 0.1m, 0.2m);
        }
    }
}
