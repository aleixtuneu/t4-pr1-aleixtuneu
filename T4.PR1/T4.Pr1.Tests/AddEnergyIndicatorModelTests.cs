using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using T4.PR1.Pages;
using T4.PR1.Model;
using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http; 

namespace T4.PR1.Tests
{
    public class AddEnergyIndicatorModelTests
    {
        private string _tempFilePath;

        public AddEnergyIndicatorModelTests()
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
        public void OnGet_LoadsDefaultValues_WhenFileExistsAndHasData()
        {
            // Arrange
            // Crear un fitxer temporal amb dades de prova
            var initialData = new List<EnergyIndicator> { new EnergyIndicator { Date = "01/2023" } };
            string initialJson = JsonConvert.SerializeObject(initialData);
            File.WriteAllText(_tempFilePath, initialJson);

            var pageModel = new AddEnergyIndicatorModel();
            pageModel.filePath = _tempFilePath; // Assignar el fitxer temporal al filePath

            // Act
            pageModel.OnGet();

            // Assert
            Assert.Equal("01/2023", pageModel.NewEnergyIndicator.Date);
            Assert.Null(pageModel.ErrorMessage);
        }

        [Fact]
        public void OnGet_SetsErrorMessage_WhenFileDoesNotExist()
        {
            // Arrange
            // Assegurar que el fitxer no existeixi
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }

            var pageModel = new AddEnergyIndicatorModel();
            pageModel.filePath = _tempFilePath; // Assignar el fitxer temporal al filePath

            // Act
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error en carregar els valors per defecte.", pageModel.ErrorMessage);
        }


        [Fact]
        public void OnPost_RedirectsToViewEnergyIndicators_WhenModelIsValidAndSaveSuccessful()
        {
            // Arrange
            var pageModel = new AddEnergyIndicatorModel();

            // Setup PageContext manually per tal de simular el ModelState
            var httpContext = new DefaultHttpContext();
            var metadataProvider = new EmptyModelMetadataProvider();
            var modelState = new ModelStateDictionary();
            var viewData = new ViewDataDictionary(metadataProvider, modelState);

            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData  // Assignar el ViewData correctament
            };
            pageModel.PageContext = pageContext;


            pageModel.filePath = _tempFilePath;  // Assignar el fitxer temporal al filePath
            pageModel.NewEnergyIndicator = new EnergyIndicator { Date = "02/2023", CDEEBC_NetProduction = 100 };

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.Equal("/ViewEnergyIndicators", redirectToPageResult.PageName);

            // Verificar que les dades s'han desat al fitxer
            string savedJson = File.ReadAllText(_tempFilePath);
            var savedIndicators = JsonConvert.DeserializeObject<List<EnergyIndicator>>(savedJson);
            Assert.Single(savedIndicators);
            Assert.Equal("02/2023", savedIndicators[0].Date);
        }


        [Fact]
        public void OnPost_ReturnsPage_WhenModelIsInvalid()
        {
            // Arrange
            var pageModel = new AddEnergyIndicatorModel();

            // Simular un model invàlid
            var httpContext = new DefaultHttpContext();
            var metadataProvider = new EmptyModelMetadataProvider();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Date", "Date is required");  // Simular un error de validació en el camp "Date"
            var viewData = new ViewDataDictionary(metadataProvider, modelState);


            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData // Assignar el ViewData correctament
            };
            pageModel.PageContext = pageContext;

            pageModel.filePath = _tempFilePath;  // Assignar el fitxer temporal al filePath
            pageModel.NewEnergyIndicator = new EnergyIndicator { Date = null }; // Model is invalid

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);

            // Verificar que el fitxer no s'hagi modificat (o estigui buit)
            Assert.False(File.Exists(_tempFilePath) && new FileInfo(_tempFilePath).Length > 0);
        }


        [Fact]
        public void OnPost_SetsErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            var pageModel = new AddEnergyIndicatorModel();

            // Simular un model vàlid
            var httpContext = new DefaultHttpContext();
            var metadataProvider = new EmptyModelMetadataProvider();
            var modelState = new ModelStateDictionary();
            var viewData = new ViewDataDictionary(metadataProvider, modelState);


            var pageContext = new PageContext()
            {
                HttpContext = httpContext,
                ViewData = viewData // Assignar el ViewData correctament
            };
            pageModel.PageContext = pageContext;

            // Simular un error d'escriptura al fitxer fent-lo de només lectura
            File.Create(_tempFilePath).Close();
            File.SetAttributes(_tempFilePath, File.GetAttributes(_tempFilePath) | FileAttributes.ReadOnly);


            pageModel.filePath = _tempFilePath;  // Assignar el fitxer temporal al filePath
            pageModel.NewEnergyIndicator = new EnergyIndicator { Date = "02/2023", CDEEBC_NetProduction = 100 };

            // Act
            IActionResult result = pageModel.OnPost();

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("Error al desar les dades.", pageModel.ErrorMessage);
        }

    }
}