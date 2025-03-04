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

        public string ErrorMessage;
        public void OnGet()
        {
        }

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
