using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using T4.PR1.Model;

namespace T4.PR1.Pages
{
    public class ViewSimulationsModel : PageModel
    {
        public string FileErrorMessage;

        public List<EnergySimulation> Simulations { get; set; } = new List<EnergySimulation>();
        
        public void OnGet()
        {
            string filePath = @"ModelData\simulacions_energia.csv";

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        try
                        {
                            var energySimulation = new EnergySimulation
                            {
                                SimulationDate = csv.GetField<DateTime>(0),
                                SystemType = csv.GetField<string>(1),
                                InputValue = csv.GetField<decimal>(2),
                                Ratio = csv.GetField<decimal>(3),
                                EnergyGenerated = csv.GetField<decimal>(4),
                                CostPerKWh = csv.GetField<decimal>(5),
                                PricePerKWh = csv.GetField<decimal>(6)
                            };
                            Simulations.Add(energySimulation);
                        }
                        catch
                        {
                            FileErrorMessage = "Error en llegir una l�nia del fitxer.";
                        }
                    }
                }
            }
            catch
            {
                FileErrorMessage = "Error de c�rrega de dades.";
            }
        }
    }
}
