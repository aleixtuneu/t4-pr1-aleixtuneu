using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using T4.PR1.Pages;
using T4.PR1.Model;
using Xunit;
using System.IO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace T4.PR1.Tests
{
    public class AddWaterConsumptionModelTests
    {
        private string _tempFilePath;

        public AddWaterConsumptionModelTests()
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
        public void OnGet_ReturnsPageSuccessfully()
        {
            // Arrange
            var pageModel = new AddWaterConsumptionModel();

            // Act
            pageModel.OnGet();

            // Assert
            // Com que OnGet està buit, verifiquem que no llença cap excepció.
            Assert.True(true);
        }

        [Fact]
        public void OnPost_RedirectsToViewWaterConsumptions_WhenModelIsValidAndSaveSuccessful_AndFileDoesNotExist()
        {
            // Arrange
            var pageModel = new AddWaterConsumptionModel();
            pageModel.NewWaterConsumption = new WaterConsumption
            {
                Year = 2023,
                Code = 8001,
                County = "Anoia",
                Population = 120000,
                HomeNetwork = 80000,
                EconomicActivities = 40000,
                TotalWaterConsumption = 1000000,
                DomesticConsumptionPerCapita = 8.33M
            };

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

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.Equal("/ViewWaterConsumptions", redirectToPageResult.PageName);

            // Verificar que les dades s'han desat al fitxer
            XDocument doc = XDocument.Load(_tempFilePath);
            Assert.Equal("WaterConsumptions", doc.Root.Name);
            Assert.Single(doc.Root.Elements("WaterConsumption"));

            XElement waterConsumption = doc.Root.Element("WaterConsumption");
            Assert.Equal("2023", waterConsumption.Element("Year").Value);
            Assert.Equal("8001", waterConsumption.Element("Code").Value);
            Assert.Equal("Anoia", waterConsumption.Element("County").Value);
        }

        [Fact]
        public void OnPost_RedirectsToViewWaterConsumptions_WhenModelIsValidAndSaveSuccessful_AndFileExists()
        {
            // Arrange
            // Crear un fitxer XML preexistent
            XElement preExistingData = new XElement("WaterConsumptions",
                new XElement("WaterConsumption",
                    new XElement("Year", 2022),
                    new XElement("Code", 8002),
                    new XElement("County", "Bages"),
                    new XElement("Population", 180000),
                    new XElement("HomeNetwork", 90000),
                    new XElement("EconomicActivities", 60000),
                    new XElement("TotalWaterConsumption", 1500000),
                    new XElement("DomesticConsumptionPerCapita", 8.33M)
                )
            );
            preExistingData.Save(_tempFilePath);

            var pageModel = new AddWaterConsumptionModel();
            pageModel.NewWaterConsumption = new WaterConsumption
            {
                Year = 2023,
                Code = 8001,
                County = "Anoia",
                Population = 120000,
                HomeNetwork = 80000,
                EconomicActivities = 40000,
                TotalWaterConsumption = 1000000,
                DomesticConsumptionPerCapita = 8.33M
            };

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

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.Equal("/ViewWaterConsumptions", redirectToPageResult.PageName);

            // Verificar que les dades s'han desat al fitxer
            XDocument doc = XDocument.Load(_tempFilePath);
            Assert.Equal("WaterConsumptions", doc.Root.Name);
            Assert.Equal(2, doc.Root.Elements("WaterConsumption").Count());

            XElement waterConsumption1 = doc.Root.Elements("WaterConsumption").First();
            Assert.Equal("2022", waterConsumption1.Element("Year").Value);
            XElement waterConsumption2 = doc.Root.Elements("WaterConsumption").Last();
            Assert.Equal("2023", waterConsumption2.Element("Year").Value);
        }

        [Fact]
        public void OnPost_ReturnsPage_WhenModelIsInvalid()
        {
            // Arrange
            var pageModel = new AddWaterConsumptionModel();

            // Simular un model invàlid
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Year", "Aquest camp és obligatori");  // Simular un error de validació en el camp "Year"
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState);
            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData
            };
            pageModel.PageContext = pageContext;

            pageModel.NewWaterConsumption = new WaterConsumption { Year = 0, Code = 8001 }; // Model is invalid

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);

            // Verificar que el fitxer no s'hagi modificat (o estigui buit)
            Assert.False(File.Exists(_tempFilePath) && new FileInfo(_tempFilePath).Length > 0);
        }

        [Fact]
        public void OnPost_ReturnsPage_WhenExceptionIsThrown()
        {
            // Arrange
            var pageModel = new AddWaterConsumptionModel();

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

            pageModel.NewWaterConsumption = new WaterConsumption
            {
                Year = 2023,
                Code = 8001,
                County = "Anoia",
                Population = 120000,
                HomeNetwork = 80000,
                EconomicActivities = 40000,
                TotalWaterConsumption = 1000000,
                DomesticConsumptionPerCapita = 8.33M
            };

            // Simular un error d'escriptura al fitxer fent-lo de només lectura
            File.Create(_tempFilePath).Close();
            File.SetAttributes(_tempFilePath, File.GetAttributes(_tempFilePath) | FileAttributes.ReadOnly);

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);

            // Verificar que el fitxer no s'hagi modificat (o estigui buit)
            Assert.False(File.Exists(_tempFilePath) && new FileInfo(_tempFilePath).Length > 0);
        }
    }
}