using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T4.PR1.Model;
using System.Globalization;
using CsvHelper;
using System.Xml.Linq;


namespace T4.PR1.Pages
{
    public class ViewWaterConsumptionsModel : PageModel
    {
        public string FileErrorMessage;

        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();

		public List<WaterConsumption> TopTenMunicipalities { get; set; } = new List<WaterConsumption>();
		public Dictionary<string, decimal> AvgConsumptionPerCounty { get; set; } = new();
		public List<WaterConsumption> SuspiciousConsumptions { get; set; } = new List<WaterConsumption>();
		public List<string> GrowingConsumptionMunicipalities { get; set; } = new List<string>();

		public void OnGet()
        {
			string filePath = @"ModelData\consum_aigua_cat_per_comarques.csv";


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
							var waterConsumption = new WaterConsumption()
							{
								Year = csv.GetField<int>(0),
								Code = csv.GetField<int>(1), 
								County = csv.GetField<string>(2), 
								Population = csv.GetField<int>(3), 
								HomeNetwork = csv.GetField<int>(4),
								EconomicActivities = csv.GetField<int>(5),
								TotalWaterConsumption = csv.GetField<int>(6), 
								DomesticConsumptionPerCapita = csv.GetField<decimal>(7) 
							};
							WaterConsumptions.Add(waterConsumption);
						}
						catch
						{
							FileErrorMessage = "Error al carregar als atributs d'un consum d'aigua";
						}
					}

					if (WaterConsumptions.Count > 0)
					{
						var latestYear = WaterConsumptions.Max(x => x.Year);

						// Top 10 municipis amb més consum en l'últim any
						TopTenMunicipalities = WaterConsumptions
							.Where(x => x.Year == latestYear)
							.OrderByDescending(x => x.TotalWaterConsumption)
							.Take(10)
							.ToList();

						// Consum mig per comarca
						AvgConsumptionPerCounty = WaterConsumptions
							.GroupBy(x => x.County)
							.ToDictionary(
								g => g.Key, 
								g => (decimal)g.Average(x => x.TotalWaterConsumption));

                        // Consums sospitosos (majors a 1000000 o iguals)
                        SuspiciousConsumptions = WaterConsumptions
							.Where(x => x.TotalWaterConsumption >= 1000000)
							.ToList();

                        // Municipis amb consum creixent en els últims 5 anys
                        GrowingConsumptionMunicipalities = WaterConsumptions
							.GroupBy(x => x.County)
							.Where(g =>
							{
								var last5Years = g.OrderByDescending(x => x.Year).Take(5).Select(x => x.TotalWaterConsumption).ToList();
								return last5Years.Zip(last5Years.Skip(1), (a, b) => a > b).All(increasing => increasing);
							})
							.Select(g => g.Key)
							.ToList();
					}
				}
			}
			catch
			{
				FileErrorMessage = "Error de càrrega de dades";
			}
		}
    }
}
