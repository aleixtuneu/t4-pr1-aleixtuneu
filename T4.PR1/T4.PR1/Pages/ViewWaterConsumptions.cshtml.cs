using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T4.PR1.Model;
using System.Security.Cryptography.X509Certificates;
using FileWorking = System.IO;
using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System;

namespace T4.PR1.Pages
{
    public class ViewWaterConsumptionsModel : PageModel
    {
        public string FileErrorMessage;

        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();

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
				}
			}
			catch
			{
				FileErrorMessage = "Error de càrrega de dades";
			}
		}
    }
}
