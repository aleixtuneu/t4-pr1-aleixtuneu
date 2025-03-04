using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    public class AddWaterConsumptionModel : PageModel
    {
        [BindProperty]
        public WaterConsumption NewWaterConsumption { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Comprova si el formulari té errors de validació
            if (ModelState.IsValid)
            {
                // Ruta del fitxer XML on es desen els consums d'aigua
                string xmlPath = @"ModelData\consum_aigua_cat_per_comarques.xml";

                // Nou element XML per representar el nou registre
                XElement newWaterConsumptionElement = new XElement("WaterConsumption",
					new XElement("Year", NewWaterConsumption.Year),
					new XElement("Code", NewWaterConsumption.Code),
					new XElement("County", NewWaterConsumption.County),
					new XElement("Population", NewWaterConsumption.Population),
					new XElement("HomeNetwork", NewWaterConsumption.HomeNetwork),
					new XElement("EconomicActivities", NewWaterConsumption.EconomicActivities),
					new XElement("TotalWaterConsumption", NewWaterConsumption.TotalWaterConsumption),
					new XElement("DomesticConsumptionPerCapita", NewWaterConsumption.DomesticConsumptionPerCapita)
				);

                // Comprova si el fitxer XML ja existeix
                if (System.IO.File.Exists(xmlPath))
                {
                    // Carrega l'arrel de l'XML i afegeix el nou registre
                    XElement root = XElement.Load(xmlPath);
                    root.Add(newWaterConsumptionElement);
                    root.Save(xmlPath); // Guarda els canvis
                }
                else
                {
                    // Si no existeix, crea un nou document XML amb el primer registre
                    XElement root = new XElement("WaterConsumptions", newWaterConsumptionElement);
                    root.Save(xmlPath); // Guarda el nou fitxer XML
                }

                // Tornar a la pàgina
                return RedirectToPage("/ViewWaterConsumptions");
			}

            return Page();
        }
    }
}
