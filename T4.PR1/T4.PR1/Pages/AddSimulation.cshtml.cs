using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    /// <summary>
    /// Pàgina Razor per afegir noves simulacions energètiques a un fitxer CSV.
    /// </summary>
    public class AddSimulationModel : PageModel
    {
        /// <summary>
        /// Obté o estableix la simulació energètica que es crearà a partir de l'entrada de l'usuari.
        /// </summary>
        /// <value>Un objecte <see cref="EnergySimulation"/> que representa les dades entrades per l'usuari.</value>
        [BindProperty]
        public EnergySimulation NewSimulation { get; set; }

        /// <summary>
        /// Obté o estableix el missatge d'error que es mostrarà a la pàgina si hi ha algun problema.
        /// </summary>
        /// <value>Una string que conté el missatge d'error, o null si no hi ha errors.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Mètode que es crida quan es carrega la pàgina.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// Mètode que es crida quan es fa un POST a la pàgina (quan l'usuari envia el formulari).
        /// Desa les dades de la simulació energètica al fitxer CSV.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que pot ser:
        ///   - Un <see cref="PageResult"/> si hi ha errors de validació o en desar les dades.
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
                ErrorMessage = "Error en desar la simulació.";
                return Page();
            }
        }
    }
}
