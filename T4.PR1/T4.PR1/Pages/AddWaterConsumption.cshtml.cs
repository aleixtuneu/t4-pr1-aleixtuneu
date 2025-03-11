using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    /// <summary>
    /// Pàgina Razor per afegir noves dades de consum d'aigua a un fitxer XML.
    /// </summary>
    public class AddWaterConsumptionModel : PageModel
    {
        /// <summary>
        /// Obté o estableix el consum d'aigua que es crearà a partir de l'entrada de l'usuari.
        /// </summary>
        /// <value>Un objecte <see cref="WaterConsumption"/> que representa les dades entrades per l'usuari.</value>
        [BindProperty]
        public WaterConsumption NewWaterConsumption { get; set; }

        /// <summary>
        /// Mètode que es crida quan es carrega la pàgina.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// Mètode que es crida quan es fa un POST a la pàgina (quan l'usuari envia el formulari).
        /// Desa les dades de consum d'aigua al fitxer XML.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que pot ser:
        ///   - Un <see cref="PageResult"/> si hi ha errors de validació.
        ///   - Un <see cref="RedirectToPageResult"/> si les dades s'han desat correctament.
        /// </returns>
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