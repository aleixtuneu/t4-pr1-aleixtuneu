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
    public class ViewEnergyIndicatorsModelTests
    {
        private string _tempFilePath;

        public ViewEnergyIndicatorsModelTests()
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
            string csvData = @"Date,PBEE_Hydroelectric,PBEE_Coal,PBEE_NaturalGas,PBEE_FuelOil,PBEE_CombinedCycle,PBEE_Nuclear,CDEEBC_GrossProduction,CDEEBC_AuxiliaryConsumption,CDEEBC_NetProduction,CDEEBC_PumpConsumption,CDEEBC_AvailableProduction,CDEEBC_TotalSalesCentralGrid,CDEEBC_InterchangeBalance,CDEEBC_ElectricityDemand,CDEEBC_TotalRegulatedMarket,CDEEBC_TotalLiberalizedMarket,FEE_Industry,FEE_Tertiary,FEE_Domestic,FEE_Primary,FEE_Energy,FEEI_PublicWorksConsumption,FEEI_SteelFoundry,FEEI_Metallurgy,FEEI_GlassIndustry,FEEI_CementLimePlaster,FEEI_OtherConstructionMaterials,FEEI_ChemicalPetrochemical,FEEI_TransportConstruction,FEEI_OtherMetalTransformation,FEEI_FoodBeverageTobacco,FEEI_TextileLeatherFootwear,FEEI_PaperPulpCardboard,FEEI_OtherIndustries,DGGN_FrontierEnagas,DGGN_GNLDistribution,DGGN_NaturalGasConsumption,CCAC_AutoGasoline,CCAC_DieselA
01/2023,100,50,200,25,150,300,1000,50,950,10,940,900,20,1000,400,600,300,200,150,50,20,10,5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85
02/2023,110,55,220,27,165,330,1100,55,1045,11,1034,990,22,1100,440,660,330,220,165,55,22,11,6,11,16,22,27,33,38,44,49,55,61,66,72,77,83,88,93";
            File.WriteAllText(_tempFilePath, csvData);

            var pageModel = new ViewEnergyIndicatorsModel();
            pageModel.OnGet();

            // Assert
            Assert.Null(pageModel.FileErrorMessage);
            Assert.Equal(2, pageModel.EnergyIndicators.Count);

            // Verificar que les anàlisis estadístiques s'han realitzat correctament
            Assert.Equal(2, pageModel.HighNetProduction.Count);
            Assert.Equal(0, pageModel.HighGasolineConsumption.Count); //No n'hi ha cap que superi 100
            Assert.Equal(2, pageModel.AvgNetProductionPerYear.Count);
            Assert.Equal(0, pageModel.HighDemandLowProduction.Count); //No n'hi ha cap que compleixi la condició
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

            var pageModel = new ViewEnergyIndicatorsModel();
            pageModel.OnGet();

            // Assert
            Assert.Equal("Error de càrrega de dades", pageModel.FileErrorMessage);
        }
    }
}
