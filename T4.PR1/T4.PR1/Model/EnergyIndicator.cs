using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
    /// <summary>
    /// Representa un indicador energètic amb dades de producció, consum i altres factors relacionats amb l'energia.
    /// </summary>
    public class EnergyIndicator
    {
        /// <summary>
        /// Obté o estableix la data de l'indicador energètic.
        /// </summary>
        /// <value>La data de l'indicador energètic en format MM/YYYY. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "El format de la data ha de ser MM/YYYY")]
        public string Date { get; set; }

        /// <summary>
        /// Obté o estableix la producció hidroelèctrica (PBEE_Hydroelectric).
        /// </summary>
        /// <value>La producció hidroelèctrica. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Hydroelectric { get; set; }

        /// <summary>
        /// Obté o estableix la producció de carbó (PBEE_Coal).
        /// </summary>
        /// <value>La producció de carbó. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Coal { get; set; }

        /// <summary>
        /// Obté o estableix la producció de gas natural (PBEE_NaturalGas).
        /// </summary>
        /// <value>La producció de gas natural. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_NaturalGas { get; set; }

        /// <summary>
        /// Obté o estableix la producció d'oli combustible (PBEE_FuelOil).
        /// </summary>
        /// <value>La producció d'oli combustible. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_FuelOil { get; set; }

        /// <summary>
        /// Obté o estableix la producció de cicle combinat (PBEE_CombinedCycle).
        /// </summary>
        /// <value>La producció de cicle combinat. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_CombinedCycle { get; set; }

        /// <summary>
        /// Obté o estableix la producció nuclear (PBEE_Nuclear).
        /// </summary>
        /// <value>La producció nuclear. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? PBEE_Nuclear { get; set; }

        /// <summary>
        /// Obté o estableix la producció bruta (CDEEBC_GrossProduction).
        /// </summary>
        /// <value>La producció bruta. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_GrossProduction { get; set; }

        /// <summary>
        /// Obté o estableix el consum auxiliar (CDEEBC_AuxiliaryConsumption).
        /// </summary>
        /// <value>El consum auxiliar. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_AuxiliaryConsumption { get; set; }

        /// <summary>
        /// Obté o estableix la producció neta (CDEEBC_NetProduction).
        /// </summary>
        /// <value>La producció neta. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_NetProduction { get; set; }

        /// <summary>
        /// Obté o estableix el consum de bombament (CDEEBC_PumpConsumption).
        /// </summary>
        /// <value>El consum de bombament. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_PumpConsumption { get; set; }

        /// <summary>
        /// Obté o estableix la producció disponible (CDEEBC_AvailableProduction).
        /// </summary>
        /// <value>La producció disponible. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_AvailableProduction { get; set; }

        /// <summary>
        /// Obté o estableix el total de vendes a la xarxa central (CDEEBC_TotalSalesCentralGrid).
        /// </summary>
        /// <value>El total de vendes a la xarxa central. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_TotalSalesCentralGrid { get; set; }

        /// <summary>
        /// Obté o estableix el balanç d'intercanvi (CDEEBC_InterchangeBalance).
        /// </summary>
        /// <value>El balanç d'intercanvi. Aquest camp és obligatori.</value>
        [Required]
        public decimal? CDEEBC_InterchangeBalance { get; set; }

        /// <summary>
        /// Obté o estableix la demanda d'electricitat (CDEEBC_ElectricityDemand).
        /// </summary>
        /// <value>La demanda d'electricitat. Aquest camp és obligatori.</value>
        [Required]
        public decimal? CDEEBC_ElectricityDemand { get; set; }

        /// <summary>
        /// Obté o estableix el total del mercat regulat (CDEEBC_TotalRegulatedMarket).
        /// </summary>
        /// <value>El total del mercat regulat. Aquest camp és obligatori i ha d'estar entre 0 i 100.</value>
        [Required, Range(0, 100)]
        public decimal? CDEEBC_TotalRegulatedMarket { get; set; }

        /// <summary>
        /// Obté o estableix el total del mercat liberalitzat (CDEEBC_TotalLiberalizedMarket).
        /// </summary>
        /// <value>El total del mercat liberalitzat. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CDEEBC_TotalLiberalizedMarket { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria (FEE_Industry).
        /// </summary>
        /// <value>El consum d'energia per la indústria. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Industry { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia pel sector terciari (FEE_Tertiary).
        /// </summary>
        /// <value>El consum d'energia pel sector terciari. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Tertiary { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia domèstic (FEE_Domestic).
        /// </summary>
        /// <value>El consum d'energia domèstic. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Domestic { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia pel sector primari (FEE_Primary).
        /// </summary>
        /// <value>El consum d'energia pel sector primari. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Primary { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia (FEE_Energy).
        /// </summary>
        /// <value>El consum d'energia. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEE_Energy { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per obres públiques (FEEI_PublicWorksConsumption).
        /// </summary>
        /// <value>El consum d'energia per obres públiques. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_PublicWorksConsumption { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la fosa d'acer (FEEI_SteelFoundry).
        /// </summary>
        /// <value>El consum d'energia per la fosa d'acer. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_SteelFoundry { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la metal·lúrgia (FEEI_Metallurgy).
        /// </summary>
        /// <value>El consum d'energia per la metal·lúrgia. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_Metallurgy { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria del vidre (FEEI_GlassIndustry).
        /// </summary>
        /// <value>El consum d'energia per la indústria del vidre. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_GlassIndustry { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la fabricació de ciment, calç i guix (FEEI_CementLimePlaster).
        /// </summary>
        /// <value>El consum d'energia per la fabricació de ciment, calç i guix. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_CementLimePlaster { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la fabricació d'altres materials de construcció (FEEI_OtherConstructionMaterials).
        /// </summary>
        /// <value>El consum d'energia per la fabricació d'altres materials de construcció. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherConstructionMaterials { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria química i petroquímica (FEEI_ChemicalPetrochemical).
        /// </summary>
        /// <value>El consum d'energia per la indústria química i petroquímica. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_ChemicalPetrochemical { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la construcció de transport (FEEI_TransportConstruction).
        /// </summary>
        /// <value>El consum d'energia per la construcció de transport. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_TransportConstruction { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la transformació d'altres metalls (FEEI_OtherMetalTransformation).
        /// </summary>
        /// <value>El consum d'energia per la transformació d'altres metalls. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherMetalTransformation { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria alimentària, de begudes i tabac (FEEI_FoodBeverageTobacco).
        /// </summary>
        /// <value>El consum d'energia per la indústria alimentària, de begudes i tabac. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_FoodBeverageTobacco { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria tèxtil, de cuir i calçat (FEEI_TextileLeatherFootwear).
        /// </summary>
        /// <value>El consum d'energia per la indústria tèxtil, de cuir i calçat. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_TextileLeatherFootwear { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per la indústria de paper, pasta de paper i cartró (FEEI_PaperPulpCardboard).
        /// </summary>
        /// <value>El consum d'energia per la indústria de paper, pasta de paper i cartró. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_PaperPulpCardboard { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'energia per altres indústries (FEEI_OtherIndustries).
        /// </summary>
        /// <value>El consum d'energia per altres indústries. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? FEEI_OtherIndustries { get; set; }

        /// <summary>
        /// Obté o estableix el consum de gas natural a la frontera Enagás (DGGN_FrontierEnagas).
        /// </summary>
        /// <value>El consum de gas natural a la frontera Enagás. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_FrontierEnagas { get; set; }

        /// <summary>
        /// Obté o estableix el consum de GNL distribuït (DGGN_GNLDistribution).
        /// </summary>
        /// <value>El consum de GNL distribuït. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_GNLDistribution { get; set; }

        /// <summary>
        /// Obté o estableix el consum total de gas natural (DGGN_NaturalGasConsumption).
        /// </summary>
        /// <value>El consum total de gas natural. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? DGGN_NaturalGasConsumption { get; set; }

        /// <summary>
        /// Obté o estableix el consum d'autogasolina (CCAC_AutoGasoline).
        /// </summary>
        /// <value>El consum d'autogasolina. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CCAC_AutoGasoline { get; set; }

        /// <summary>
        /// Obté o estableix el consum de dièsel A (CCAC_DieselA).
        /// </summary>
        /// <value>El consum de dièsel A. Aquest camp és obligatori i ha de ser major o igual a 0.</value>
        [Required, Range(0, double.MaxValue)]
        public decimal? CCAC_DieselA { get; set; }
    }
}