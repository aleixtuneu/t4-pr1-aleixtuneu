using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    public class AddSimulationModel : PageModel
    {
        [BindProperty]
        public EnergySimulation NewSimulation { get; set; }

        // Missatge d'error de problemes amb la validació
        public string ErrorMessage;
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Comprova si el formulari té errors de validació i retorna a la pàgina indicant els errors
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Si us plau, omple tots els camps correctament.";
                return Page();
            }

            try
            {
                // Ruta del csv on es guarden les simulacions
                string filePath = @"ModelData\simulacions_energia.csv";
                
                bool fileExists = System.IO.File.Exists(filePath);

                // Escriu al fitxer en mode d'afegir, append: true
                using (var writer = new StreamWriter(filePath, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Si el fitxer no existeix, escriu la capçalera abans d'afegir registres
                    if (!fileExists)
                    {
                        csv.WriteHeader<EnergySimulation>();
                        csv.NextRecord();
                    }

                    // Escriure la nova simulació al csv
                    csv.WriteRecord(NewSimulation);
                    csv.NextRecord();
                }

                return RedirectToPage("ViewSimulations");
            }
            catch
            {
                ErrorMessage = "Error en desar la simulació.";
                return Page();
            }
        }
    }
}
