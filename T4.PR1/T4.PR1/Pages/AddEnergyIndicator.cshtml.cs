using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using T4.PR1.Model;
using Newtonsoft.Json;

namespace T4.PR1.Pages
{
    public class AddEnergyIndicatorModel : PageModel
    {
        // Propietat vinculada al formulari per introdu�r un nou indicador d'energia
        [BindProperty]
        public EnergyIndicator NewEnergyIndicator { get; set; }

        // Missatge d'error per indicar problemes relacionats amb la validaci�
        public string ErrorMessage { get; set; }

        // Ruta del fitxer json on es guarden els valors
        public string filePath = @"ModelData\indicadors_energetics_cat.json";
        
        public void OnGet()
        {
        }

        // Gesti� de l'enviament del formulari
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();  // Si el formulari t� errors retorna a la p�gina amb els errors
            }

            // Llista d'indicadors existents
            List<EnergyIndicator> energyIndicators = new List<EnergyIndicator>();

            try
            {
                // Comprova si el fitxer JSON ja existeix
                if (System.IO.File.Exists(filePath))
                {
                    // Llegeix el contingut del fitxer JSON i Deserialitza el JSON en una llista d'objectes EnergyIndicator 
                    string json = System.IO.File.ReadAllText(filePath);
                    energyIndicators = JsonConvert.DeserializeObject<List<EnergyIndicator>>(json) ?? new List<EnergyIndicator>();
                }

                // Afegir nou indicador a la llista
                energyIndicators.Add(NewEnergyIndicator);

                // Serialitza la llista actualitzada a JSON i escriu al fitxer
                string updatedJson = JsonConvert.SerializeObject(energyIndicators, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, updatedJson);
            }
            catch
            {
                ErrorMessage = "Error al desar les dades.";
                return Page();
            }

            // Retorna a la p�gina
            return RedirectToPage("/ViewEnergyIndicators");
        }
    }
}
