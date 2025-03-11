using Microsoft.AspNetCore.Mvc.RazorPages;
using T4.PR1.Pages;
using Xunit;
using System.IO;
using CsvHelper;
using System.Globalization;
using T4.PR1.Model;
using System.Collections.Generic;
using System.Linq;

namespace T4.PR1.Tests
{
    public class ViewWaterConsumptionsModelTests
    {
        private string _tempFilePath;

        public ViewWaterConsumptionsModelTests()
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
            string csvData = @"Year,Code,County,Population,HomeNetwork,EconomicActivities,TotalWaterConsumption,DomesticConsumptionPerCapita
2023,1,ComarcaA,1000,500,200,1200000,10.5
2023,2,ComarcaB,1500,750,300,800000,8.2
2022,3,ComarcaA,900,450,180,900000,9.0
2022,4,ComarcaC,1200,600,240,1500000,12.5
2022,5,ComarcaB,1400,700,280,700000,7.8";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewWaterConsumptionsModel();
            pageModel.OnGet();

            // Assert
            Assert.Null(pageModel.FileErrorMessage);
            Assert.Equal(5, pageModel.WaterConsumptions.Count);

            //Verificar TopTenMunicipalities
            Assert.Equal(2, pageModel.TopTenMunicipalities.Count); //Només 2023
            Assert.Equal("ComarcaA", pageModel.TopTenMunicipalities[0].County);
            Assert.Equal("ComarcaB", pageModel.TopTenMunicipalities[1].County);

            //Verificar AvgConsumptionPerCounty
            Assert.Equal(3, pageModel.AvgConsumptionPerCounty.Count);
            Assert.Equal(1050000, pageModel.AvgConsumptionPerCounty["ComarcaA"]);

            //Verificar SuspiciousConsumptions
            Assert.Equal(2, pageModel.SuspiciousConsumptions.Count);
            Assert.Contains(pageModel.SuspiciousConsumptions, x => x.County == "ComarcaA" && x.Year == 2023);
            Assert.Contains(pageModel.SuspiciousConsumptions, x => x.County == "ComarcaC" && x.Year == 2022);

            //Verificar GrowingConsumptionMunicipalities
            Assert.Empty(pageModel.GrowingConsumptionMunicipalities);
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

            var pageModel = new ViewWaterConsumptionsModel();
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error de càrrega de dades", pageModel.FileErrorMessage);
        }

        [Fact]
        public void OnGet_SetsFileErrorMessage_WhenFileHasInvalidData()
        {
            // Arrange
            // Crear un fitxer CSV amb dades incorrectes
            string csvData = @"Year,Code,County,Population,HomeNetwork,EconomicActivities,TotalWaterConsumption,DomesticConsumptionPerCapita
Invalid,1,ComarcaA,1000,500,200,1200000,10.5";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewWaterConsumptionsModel();
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error al carregar als atributs d'un consum d'aigua", pageModel.FileErrorMessage);
            Assert.Empty(pageModel.WaterConsumptions);
        }
    }
}