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
        public EnergyIndicator NewEnergyIndicator { get; set; }

        public string ErrorMessage { get; set; }

        public string filePath = @"ModelData\indicadors_energetics_cat.json";
        
        public void OnGet()
        {
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
                if (System.IO.File.Exists(filePath))
                {
                    string json = System.IO.File.ReadAllText(filePath);
                    energyIndicators = JsonConvert.DeserializeObject<List<EnergyIndicator>>(json) ?? new List<EnergyIndicator>();
                }

                energyIndicators.Add(NewEnergyIndicator);

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
