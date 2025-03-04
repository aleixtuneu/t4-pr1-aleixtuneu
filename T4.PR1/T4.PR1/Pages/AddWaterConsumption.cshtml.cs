using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    }
}
