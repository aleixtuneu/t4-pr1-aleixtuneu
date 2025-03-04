using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
    public class EnergyIndicator
    {
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "El format de la data ha de ser MM/YYYY")]
        public string Date { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Hydroelectric { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Coal { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_NaturalGas { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_FuelOil { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_CombinedCycle { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Nuclear { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_GrossProduction { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_AuxiliaryConsumption { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_NetProduction { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_PumpConsumption { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_AvailableProduction { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_TotalSalesCentralGrid { get; set; }

        [Required]
        public decimal? CDEEBC_InterchangeBalance { get; set; }

        [Required]
        public decimal? CDEEBC_ElectricityDemand { get; set; }

        [Required, Range(0, 100)]
        public decimal? CDEEBC_TotalRegulatedMarket { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_TotalLiberalizedMarket { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Industry { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Tertiary { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Domestic { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Primary { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Energy { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_PublicWorksConsumption { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_SteelFoundry { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_Metallurgy { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_GlassIndustry { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_CementLimePlaster { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherConstructionMaterials { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_ChemicalPetrochemical { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_TransportConstruction { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherMetalTransformation { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_FoodBeverageTobacco { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_TextileLeatherFootwear { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_PaperPulpCardboard { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherIndustries { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_FrontierEnagas { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_GNLDistribution { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_NaturalGasConsumption { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CCAC_AutoGasoline { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? CCAC_DieselA { get; set; }
    }
}
