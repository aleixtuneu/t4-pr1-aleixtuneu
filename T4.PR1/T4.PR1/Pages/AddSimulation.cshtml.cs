using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    /// <summary>
    /// P�gina Razor per afegir noves simulacions energ�tiques a un fitxer CSV.
    /// </summary>
    public class AddSimulationModel : PageModel
    {
        /// <summary>
        /// Obt� o estableix la simulaci� energ�tica que es crear� a partir de l'entrada de l'usuari.
        /// </summary>
        /// <value>Un objecte <see cref="EnergySimulation"/> que representa les dades entrades per l'usuari.</value>
        [BindProperty]
        public EnergySimulation NewSimulation { get; set; }

        /// <summary>
        /// Obt� o estableix el missatge d'error que es mostrar� a la p�gina si hi ha algun problema.
        /// </summary>
        /// <value>Una string que cont� el missatge d'error, o null si no hi ha errors.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// M�tode que es crida quan es carrega la p�gina.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// M�tode que es crida quan es fa un POST a la p�gina (quan l'usuari envia el formulari).
        /// Desa les dades de la simulaci� energ�tica al fitxer CSV.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que pot ser:
        ///   - Un <see cref="PageResult"/> si hi ha errors de validaci� o en desar les dades.
        ///   - Un <see cref="RedirectToPageResult"/> si les dades s'han desat correctament.
        /// </returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Si us plau, omple tots els camps correctament.";
                return Page();
            }

            try
            {
                string filePath = @"ModelData\simulacions_energia.csv";
                bool fileExists = System.IO.File.Exists(filePath);

                using (var writer = new StreamWriter(filePath, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (!fileExists)
                    {
                        csv.WriteHeader<EnergySimulation>();
                        csv.NextRecord();
                    }

                    csv.WriteRecord(NewSimulation);
                    csv.NextRecord();
                }

                return RedirectToPage("ViewSimulations");
            }
            catch
            {
                ErrorMessage = "Error en desar la simulaci�.";
                return Page();
            }
        }
    }
}
