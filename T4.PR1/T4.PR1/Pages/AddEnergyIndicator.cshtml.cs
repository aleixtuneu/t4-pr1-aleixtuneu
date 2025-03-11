using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using T4.PR1.Model;
using Newtonsoft.Json;

namespace T4.PR1.Pages
{
    public class AddEnergyIndicatorModel : PageModel
    {
        [BindProperty]
        public EnergyIndicator NewEnergyIndicator { get; set; } = new EnergyIndicator();

        public string ErrorMessage { get; set; }

        public string filePath = @"ModelData\indicadors_energetics_cat.json";

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
                    CDEEBC_NetProduction = NewEnergyIndicator.CDEEBC_NetProduction,
                    CCAC_AutoGasoline = NewEnergyIndicator.CCAC_AutoGasoline,
                    CDEEBC_ElectricityDemand = NewEnergyIndicator.CDEEBC_ElectricityDemand,
                    CDEEBC_AvailableProduction = NewEnergyIndicator.CDEEBC_AvailableProduction
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