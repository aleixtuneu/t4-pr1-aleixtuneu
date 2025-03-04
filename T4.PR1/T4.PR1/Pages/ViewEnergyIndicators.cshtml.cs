using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    public class ViewEnergyIndicatorsModel : PageModel
    {
        // Missatge d'error
        public string FileErrorMessage;

        // Llista d'inidcadors
        public List<EnergyIndicator> EnergyIndicators { get; set; } = new List<EnergyIndicator>();

        // Llistes per els anàlisis estadístics
        public List<EnergyIndicator> HighNetProduction { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> HighGasolineConsumption { get; set; } = new List<EnergyIndicator>();
        public Dictionary<int, decimal> AvgNetProductionPerYear { get; set; } = new();
        public List<EnergyIndicator> HighDemandLowProduction { get; set; } = new List<EnergyIndicator>();
        
        public void OnGet()
        {
            string filePath = @"ModelData\indicadors_energetics_cat.csv";

            try
            {
                // Llegir el CSV
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read()) // Llegeix cada fila
                    {
                        try
                        {
                            // Nou objecte amb els valors llegits
                            var energyIndicator = new EnergyIndicator()
                            {
                                Date = csv.TryGetField<string>(0, out var date) ? date : "01/2000",
                                PBEE_Hydroelectric = csv.TryGetField<decimal>(1, out var val) ? val : 0,
                                PBEE_Coal = csv.TryGetField<decimal>(2, out val) ? val : 0,
                                PBEE_NaturalGas = csv.TryGetField<decimal>(3, out val) ? val : 0,
                                PBEE_FuelOil = csv.TryGetField<decimal>(4, out val) ? val : 0,
                                PBEE_CombinedCycle = csv.TryGetField<decimal>(5, out val) ? val : 0,
                                PBEE_Nuclear = csv.TryGetField<decimal>(6, out val) ? val : 0,
                                CDEEBC_GrossProduction = csv.TryGetField<decimal>(7, out val) ? val : 0,
                                CDEEBC_AuxiliaryConsumption = csv.TryGetField<decimal>(8, out val) ? val : 0,
                                CDEEBC_NetProduction = csv.TryGetField<decimal>(9, out val) ? val : 0,
                                CDEEBC_PumpConsumption = csv.TryGetField<decimal>(10, out val) ? val : 0,
                                CDEEBC_AvailableProduction = csv.TryGetField<decimal>(11, out val) ? val : 0,
                                CDEEBC_TotalSalesCentralGrid = csv.TryGetField<decimal>(12, out val) ? val : 0,
                                CDEEBC_InterchangeBalance = csv.TryGetField<decimal>(13, out val) ? val : 0,
                                CDEEBC_ElectricityDemand = csv.TryGetField<decimal>(14, out val) ? val : 0,
                                CDEEBC_TotalRegulatedMarket = csv.TryGetField<decimal>(15, out val) ? val : 0,
                                CDEEBC_TotalLiberalizedMarket = csv.TryGetField<decimal>(16, out val) ? val : 0,
                                FEE_Industry = csv.TryGetField<decimal>(17, out val) ? val : 0,
                                FEE_Tertiary = csv.TryGetField<decimal>(18, out val) ? val : 0,
                                FEE_Domestic = csv.TryGetField<decimal>(19, out val) ? val : 0,
                                FEE_Primary = csv.TryGetField<decimal>(20, out val) ? val : 0,
                                FEE_Energy = csv.TryGetField<decimal>(21, out val) ? val : 0,
                                FEEI_PublicWorksConsumption = csv.TryGetField<decimal>(22, out val) ? val : 0,
                                FEEI_SteelFoundry = csv.TryGetField<decimal>(23, out val) ? val : 0,
                                FEEI_Metallurgy = csv.TryGetField<decimal>(24, out val) ? val : 0,
                                FEEI_GlassIndustry = csv.TryGetField<decimal>(25, out val) ? val : 0,
                                FEEI_CementLimePlaster = csv.TryGetField<decimal>(26, out val) ? val : 0,
                                FEEI_OtherConstructionMaterials = csv.TryGetField<decimal>(27, out val) ? val : 0,
                                FEEI_ChemicalPetrochemical = csv.TryGetField<decimal>(28, out val) ? val : 0,
                                FEEI_TransportConstruction = csv.TryGetField<decimal>(29, out val) ? val : 0,
                                FEEI_OtherMetalTransformation = csv.TryGetField<decimal>(30, out val) ? val : 0,
                                FEEI_FoodBeverageTobacco = csv.TryGetField<decimal>(31, out val) ? val : 0,
                                FEEI_TextileLeatherFootwear = csv.TryGetField<decimal>(32, out val) ? val : 0,
                                FEEI_PaperPulpCardboard = csv.TryGetField<decimal>(33, out val) ? val : 0,
                                FEEI_OtherIndustries = csv.TryGetField<decimal>(34, out val) ? val : 0,
                                DGGN_FrontierEnagas = csv.TryGetField<decimal>(35, out val) ? val : 0,
                                DGGN_GNLDistribution = csv.TryGetField<decimal>(36, out val) ? val : 0,
                                DGGN_NaturalGasConsumption = csv.TryGetField<decimal>(37, out val) ? val : 0,
                                CCAC_AutoGasoline = csv.TryGetField<decimal>(38, out val) ? val : 0,
                                CCAC_DieselA = csv.TryGetField<decimal>(39, out val) ? val : 0
                            };
                            EnergyIndicators.Add(energyIndicator); // Afegir a la llista
                        }
                        catch
                        {
                            FileErrorMessage = "Error al carregar les dades d'un indicador energètic";
                        }
                    }

                    // Si hi ha indicadors, es realitzen anàlisis estadístics
                    if (EnergyIndicators.Count > 0)
                    {
                        // Filtra els indicadors amb una producció neta superior a 3000
                        HighNetProduction = EnergyIndicators
                            .Where(e => e.CDEEBC_NetProduction > 3000)
                            .OrderBy(e => e.CDEEBC_NetProduction)
                            .ToList();

                        // Filtra els indicadors amb un consum de gasolina superior a 100
                        HighGasolineConsumption = EnergyIndicators
                            .Where(e => e.CCAC_AutoGasoline > 100)
                            .OrderByDescending(e => e.CCAC_AutoGasoline)
                            .ToList();

                        // Calcula la producció neta mitjana per any
                        AvgNetProductionPerYear = EnergyIndicators
                            .Select(e =>
                            {
                                var parts = e.Date.Split('/'); // Separa la data en mes i any
                                return (Year: parts.Length == 2 && int.TryParse(parts[1], out int year) ? year : (int?)null, e.CDEEBC_NetProduction);
                            })
                            .Where(e => e.Year.HasValue) // Filtra només els valors amb un any vàlid
                            .GroupBy(e => e.Year.Value) // Agrupa per any
                            .ToDictionary(
                                g => g.Key,
                                g => g.Average(e => e.CDEEBC_NetProduction) ?? 0m // Calcula la mitjana de producció per any
                            )
                            .OrderBy(kv => kv.Key)
                            .ToDictionary(kv => kv.Key, kv => kv.Value); // Ordena per any

                        // Filtra registres amb alta demanda (>4000) i baixa producció (≤300)
                        HighDemandLowProduction = EnergyIndicators
                            .Where(e => e.CDEEBC_ElectricityDemand > 4000 && e.CDEEBC_AvailableProduction <= 300)
                            .ToList();
                    }
                }
            }
            catch
            {
                FileErrorMessage = "Error de càrrega de dades";
            }
        }
    }
}