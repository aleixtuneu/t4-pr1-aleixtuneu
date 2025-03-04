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
            if (ModelState.IsValid)
            {
                string xmlPath = @"ModelData\consum_aigua_cat_per_comarques.xml";

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

                if (System.IO.File.Exists(xmlPath))
                {
                    XElement root = XElement.Load(xmlPath);
                    root.Add(newWaterConsumptionElement);
                    root.Save(xmlPath);
                }
                else
                {
                    XElement root = new XElement("WaterConsumptions", newWaterConsumptionElement);
                    root.Save(xmlPath);
                }

                return RedirectToPage("/ViewWaterConsumptions");
			}

            return Page();
        }
    }
}
