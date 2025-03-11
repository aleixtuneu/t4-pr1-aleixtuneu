using Microsoft.AspNetCore.Mvc.RazorPages;
using T4.PR1.Pages;
using Xunit;
using System.IO;
using CsvHelper;
using System.Globalization;
using T4.PR1.Model;
using System.Collections.Generic;
using System;

namespace T4.PR1.Tests
{
    public class ViewSimulationsModelTests
    {
        private string _tempFilePath;

        public ViewSimulationsModelTests()
        {
            // Crear un fitxer temporal per a cada test
            _tempFilePath = Path.GetTempFileName();
        }

        // Netejar el fitxer temporal després de cada test
        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        [Fact]
        public void OnGet_LoadsDataCorrectly_WhenFileExistsAndIsValid()
        {
            // Arrange
            // Crear un fitxer CSV amb dades de prova
            string csvData = @"SimulationDate,SystemType,InputValue,Ratio,EnergyGenerated,CostPerKWh,PricePerKWh
01/01/2023,Solar,100,1.5,0,0.1,0.2
02/01/2023,Wind,50,0.8,0,0.08,0.15";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewSimulationsModel();
            pageModel.OnGet();

            // Assert
            Assert.Null(pageModel.FileErrorMessage);
            Assert.Equal(2, pageModel.Simulations.Count);

            // Verificar les dades de la primera simulació
            Assert.Equal(new DateTime(2023, 1, 1), pageModel.Simulations[0].SimulationDate);
            Assert.Equal("Solar", pageModel.Simulations[0].SystemType);
            Assert.Equal(100, pageModel.Simulations[0].InputValue);
            Assert.Equal(1.5M, pageModel.Simulations[0].Ratio);

            //Verificar que s'ha recalculat l'energia generada
            SolarSystem solar = new SolarSystem(1.5M);
            Assert.Equal(solar.CalculateEnergy(100), pageModel.Simulations[0].EnergyGenerated);

            // Verificar les dades de la segona simulació
            Assert.Equal(new DateTime(2023, 2, 1), pageModel.Simulations[1].SimulationDate);
            Assert.Equal("Wind", pageModel.Simulations[1].SystemType);
            Assert.Equal(50, pageModel.Simulations[1].InputValue);
            Assert.Equal(0.8M, pageModel.Simulations[1].Ratio);

            //Verificar que s'ha recalculat l'energia generada
            WindSystem wind = new WindSystem(0.8M);
            Assert.Equal(wind.CalculateEnergy(50), pageModel.Simulations[1].EnergyGenerated);
        }

        [Fact]
        public void OnGet_SetsFileErrorMessage_WhenFileDoesNotExist()
        {
            // Arrange
            // Assegurar que el fitxer no existeixi
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }

            var pageModel = new ViewSimulationsModel();
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error de càrrega de dades.", pageModel.FileErrorMessage);
        }

        [Fact]
        public void OnGet_SetsFileErrorMessage_WhenFileHasInvalidData()
        {
            // Arrange
            // Crear un fitxer CSV amb dades incorrectes (per exemple, un format de data incorrecte)
            string csvData = @"SimulationDate,SystemType,InputValue,Ratio,EnergyGenerated,CostPerKWh,PricePerKWh
Invalid Date,Solar,100,1.5,0,0.1,0.2";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewSimulationsModel();
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error en llegir una línia del fitxer.", pageModel.FileErrorMessage);
            Assert.Empty(pageModel.Simulations);
        }

        [Fact]
        public void OnGet_ThrowsException_WhenSystemTypeIsInvalid()
        {
            // Arrange
            // Crear un fitxer CSV amb un SystemType incorrecte
            string csvData = @"SimulationDate,SystemType,InputValue,Ratio,EnergyGenerated,CostPerKWh,PricePerKWh
01/01/2023,InvalidSystem,100,1.5,0,0.1,0.2";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewSimulationsModel();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => pageModel.OnGet());
            Assert.Equal("Error de càrrega de dades.", pageModel.FileErrorMessage);
        }
    }
}
