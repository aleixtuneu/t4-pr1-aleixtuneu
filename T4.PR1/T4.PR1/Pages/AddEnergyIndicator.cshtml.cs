using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using T4.PR1.Model;
using Newtonsoft.Json;

namespace T4.PR1.Pages
{
    /// <summary>
    /// Pàgina Razor per afegir nous indicadors energètics a un fitxer JSON.
    /// </summary>
    public class AddEnergyIndicatorModel : PageModel
    {
        /// <summary>
        /// Obté o estableix l'indicador energètic que es crearà a partir de l'entrada de l'usuari.
        /// </summary>
        /// <value>Un objecte <see cref="EnergyIndicator"/> que representa les dades entrades per l'usuari.</value>
        [BindProperty]
        public EnergyIndicator NewEnergyIndicator { get; set; } = new EnergyIndicator();

        /// <summary>
        /// Obté o estableix el missatge d'error que es mostrarà a la pàgina si hi ha algun problema.
        /// </summary>
        /// <value>Una string que conté el missatge d'error, o null si no hi ha errors.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Obté o estableix la ruta al fitxer JSON on es desaran els indicadors energètics.
        /// </summary>
        /// <value>Una string que representa la ruta al fitxer JSON.</value>
        public string filePath = @"ModelData\indicadors_energetics_cat.json";

        /// <summary>
        /// Mètode que es crida quan es carrega la pàgina. Intenta carregar els valors per defecte del fitxer JSON.
        /// </summary>
        public void OnGet()
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    string json = System.IO.File.ReadAllText(filePath);
                    var defaultValues = JsonConvert.DeserializeObject<List<EnergyIndicator>>(json);

                    if (defaultValues != null && defaultValues.Count > 0)
                    {
                        // Agafem el primer element com a default
                        NewEnergyIndicator = defaultValues[0];
                    }
                }
            }
            catch
            {
                ErrorMessage = "Error en carregar els valors per defecte.";
            }
        }

        /// <summary>
        /// Mètode que es crida quan es fa un POST a la pàgina (quan l'usuari envia el formulari).
        /// Desa les dades de l'indicador energètic al fitxer JSON.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que pot ser:
        ///   - Un <see cref="PageResult"/> si hi ha errors de validació o en desar les dades.
        ///   - Un <see cref="RedirectToPageResult"/> si les dades s'han desat correctament.
        /// </returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<EnergyIndicator> energyIndicators = new List<EnergyIndicator>();

            try
            {
                // Si l'arxiu existeix, llegir dades
                if (System.IO.File.Exists(filePath))
                {
                    string json = System.IO.File.ReadAllText(filePath);
                    energyIndicators = JsonConvert.DeserializeObject<List<EnergyIndicator>>(json) ?? new List<EnergyIndicator>();
                }

                // Crear un nou objecte amb les dades de l'usuari
                var newIndicator = new EnergyIndicator
                {
                    Date = NewEnergyIndicator.Date,
                    PBEE_Hydroelectric = NewEnergyIndicator.PBEE_Hydroelectric,
                    PBEE_Coal = NewEnergyIndicator.PBEE_Coal,
                    PBEE_NaturalGas = NewEnergyIndicator.PBEE_NaturalGas,
                    PBEE_FuelOil = NewEnergyIndicator.PBEE_FuelOil,
                    PBEE_CombinedCycle = NewEnergyIndicator.PBEE_CombinedCycle,
                    PBEE_Nuclear = NewEnergyIndicator.PBEE_Nuclear,
                    CDEEBC_GrossProduction = NewEnergyIndicator.CDEEBC_GrossProduction,
                    CDEEBC_AuxiliaryConsumption = NewEnergyIndicator.CDEEBC_AuxiliaryConsumption,
                    CDEEBC_NetProduction = NewEnergyIndicator.CDEEBC_NetProduction,
                    CDEEBC_PumpConsumption = NewEnergyIndicator.CDEEBC_PumpConsumption,
                    CDEEBC_AvailableProduction = NewEnergyIndicator.CDEEBC_AvailableProduction,
                    CDEEBC_TotalSalesCentralGrid = NewEnergyIndicator.CDEEBC_TotalSalesCentralGrid,
                    CDEEBC_InterchangeBalance = NewEnergyIndicator.CDEEBC_InterchangeBalance,
                    CDEEBC_ElectricityDemand = NewEnergyIndicator.CDEEBC_ElectricityDemand,
                    CDEEBC_TotalRegulatedMarket = NewEnergyIndicator.CDEEBC_TotalRegulatedMarket,
                    CDEEBC_TotalLiberalizedMarket = NewEnergyIndicator.CDEEBC_TotalLiberalizedMarket,
                    FEE_Industry = NewEnergyIndicator.FEE_Industry,
                    FEE_Tertiary = NewEnergyIndicator.FEE_Tertiary,
                    FEE_Domestic = NewEnergyIndicator.FEE_Domestic,
                    FEE_Primary = NewEnergyIndicator.FEE_Primary,
                    FEE_Energy = NewEnergyIndicator.FEE_Energy,
                    FEEI_PublicWorksConsumption = NewEnergyIndicator.FEEI_PublicWorksConsumption,
                    FEEI_SteelFoundry = NewEnergyIndicator.FEEI_SteelFoundry,
                    FEEI_Metallurgy = NewEnergyIndicator.FEEI_Metallurgy,
                    FEEI_GlassIndustry = NewEnergyIndicator.FEEI_GlassIndustry,
                    FEEI_CementLimePlaster = NewEnergyIndicator.FEEI_CementLimePlaster,
                    FEEI_OtherConstructionMaterials = NewEnergyIndicator.FEEI_OtherConstructionMaterials,
                    FEEI_ChemicalPetrochemical = NewEnergyIndicator.FEEI_ChemicalPetrochemical,
                    FEEI_TransportConstruction = NewEnergyIndicator.FEEI_TransportConstruction,
                    FEEI_OtherMetalTransformation = NewEnergyIndicator.FEEI_OtherMetalTransformation,
                    FEEI_FoodBeverageTobacco = NewEnergyIndicator.FEEI_FoodBeverageTobacco,
                    FEEI_TextileLeatherFootwear = NewEnergyIndicator.FEEI_TextileLeatherFootwear,
                    FEEI_PaperPulpCardboard = NewEnergyIndicator.FEEI_PaperPulpCardboard,
                    FEEI_OtherIndustries = NewEnergyIndicator.FEEI_OtherIndustries,
                    DGGN_FrontierEnagas = NewEnergyIndicator.DGGN_FrontierEnagas,
                    DGGN_GNLDistribution = NewEnergyIndicator.DGGN_GNLDistribution,
                    DGGN_NaturalGasConsumption = NewEnergyIndicator.DGGN_NaturalGasConsumption,
                    CCAC_AutoGasoline = NewEnergyIndicator.CCAC_AutoGasoline,
                    CCAC_DieselA = NewEnergyIndicator.CCAC_DieselA
                };

                // Afegir el nou objecte a la llista
                energyIndicators.Add(newIndicator);

                // Guardar al JSON
                string updatedJson = JsonConvert.SerializeObject(energyIndicators, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, updatedJson);
            }
            catch
            {
                ErrorMessage = "Error al desar les dades.";
                return Page();
            }

            return RedirectToPage("/ViewEnergyIndicators");
        }
    }
}