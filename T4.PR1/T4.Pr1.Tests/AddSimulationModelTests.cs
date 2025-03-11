using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using T4.PR1.Pages;
using T4.PR1.Model;
using Xunit;
using System.IO;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;

namespace T4.PR1.Tests
{
    public class AddSimulationModelTests
    {
        private string _tempFilePath;

        public AddSimulationModelTests()
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
        public void OnPost_RedirectsToViewSimulations_WhenModelIsValidAndSaveSuccessful_AndFileDoesNotExist()
        {
            // Arrange
            var pageModel = new AddSimulationModel();

            // Simular un model vàlid
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState);
            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData
            };
            pageModel.PageContext = pageContext;

            pageModel.NewSimulation = new EnergySimulation { SystemType = "Solar", InputValue = 100, Ratio = 1.5M, EnergyGenerated = 50, CostPerKWh = 0.1M, PricePerKWh = 0.2M };

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.Equal("ViewSimulations", redirectToPageResult.PageName);

            // Verificar que les dades s'han desat al fitxer
            using (var reader = new StreamReader(_tempFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var record = csv.GetRecord<EnergySimulation>();

                Assert.Equal("Solar", record.SystemType);
                Assert.Equal(100, record.InputValue);
                Assert.Equal(1.5M, record.Ratio);
                Assert.Equal(50, record.EnergyGenerated);
                Assert.Equal(0.1M, record.CostPerKWh);
                Assert.Equal(0.2M, record.PricePerKWh);
            }
        }

        [Fact]
        public void OnPost_RedirectsToViewSimulations_WhenModelIsValidAndSaveSuccessful_AndFileExists()
        {
            // Arrange
            //Crear un fitxer amb una simulació preexistent
            var preExistingSimulation = new EnergySimulation { SystemType = "Wind", InputValue = 50, Ratio = 0.8M, EnergyGenerated = 25, CostPerKWh = 0.08M, PricePerKWh = 0.15M };
            using (var writer = new StreamWriter(_tempFilePath, append: false)) //Sobreescriu el fitxer si existeix
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<EnergySimulation>();
                csv.NextRecord();
                csv.WriteRecord(preExistingSimulation);
                csv.NextRecord();
            }

            var pageModel = new AddSimulationModel();

            // Simular un model vàlid
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState);
            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData
            };
            pageModel.PageContext = pageContext;

            pageModel.NewSimulation = new EnergySimulation { SystemType = "Solar", InputValue = 100, Ratio = 1.5M, EnergyGenerated = 50, CostPerKWh = 0.1M, PricePerKWh = 0.2M };

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.Equal("ViewSimulations", redirectToPageResult.PageName);

            // Verificar que les dades s'han desat al fitxer (les dues simulacions)
            using (var reader = new StreamReader(_tempFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var record1 = csv.GetRecord<EnergySimulation>();
                csv.Read();
                var record2 = csv.GetRecord<EnergySimulation>();

                Assert.Equal("Wind", record1.SystemType);
                Assert.Equal("Solar", record2.SystemType);
            }
        }

        [Fact]
        public void OnPost_ReturnsPage_WhenModelIsInvalid()
        {
            // Arrange
            var pageModel = new AddSimulationModel();

            // Simular un model invàlid
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("SystemType", "El tipus de sistema és obligatori.");  // Simular un error de validació en el camp "SystemType"
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState);
            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData
            };
            pageModel.PageContext = pageContext;

            pageModel.NewSimulation = new EnergySimulation { SystemType = null, InputValue = 100, Ratio = 1.5M, EnergyGenerated = 50, CostPerKWh = 0.1M, PricePerKWh = 0.2M }; // Model is invalid

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("Si us plau, omple tots els camps correctament.", pageModel.ErrorMessage);

            // Verificar que el fitxer no s'hagi modificat (o estigui buit)
            Assert.False(File.Exists(_tempFilePath) && new FileInfo(_tempFilePath).Length > 0);
        }

        [Fact]
        public void OnPost_SetsErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            var pageModel = new AddSimulationModel();

            // Simular un model vàlid
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState);
            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData
            };
            pageModel.PageContext = pageContext;

            // Simular un error d'escriptura al fitxer fent-lo de només lectura
            File.Create(_tempFilePath).Close();
            File.SetAttributes(_tempFilePath, File.GetAttributes(_tempFilePath) | FileAttributes.ReadOnly);

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("Error en desar la simulació.", pageModel.ErrorMessage);
        }
    }
}