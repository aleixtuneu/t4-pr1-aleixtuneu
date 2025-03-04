using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
	public class WaterConsumption
	{
		[Required(ErrorMessage = "Aquest camp és obligatori")]
		[Range(1, 99999999, ErrorMessage = "L'any ha de tenir fins a 4 xifres i no pot ser 0")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public int Code { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public string? County { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public int Population { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public int HomeNetwork { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public int EconomicActivities { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public int TotalWaterConsumption { get; set; }

		[Required(ErrorMessage = "Aquest camp és obligatori")]
		public decimal DomesticConsumptionPerCapita { get; set; }
	}
}
