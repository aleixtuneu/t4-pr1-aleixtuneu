using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
    /// <summary>
    /// Representa el consum d'aigua per una comarca en un any determinat.
    /// </summary>
    public class WaterConsumption
    {
        /// <summary>
        /// Obté o estableix l'any del consum d'aigua.
        /// </summary>
        /// <value>L'any del consum d'aigua. Aquest camp és obligatori i ha de tenir fins a 4 xifres i no pot ser 0.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        [Range(1, 99999999, ErrorMessage = "L'any ha de tenir fins a 4 xifres i no pot ser 0")]
        public int Year { get; set; }

        /// <summary>
        /// Obté o estableix el codi de la comarca.
        /// </summary>
        /// <value>El codi de la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int Code { get; set; }

        /// <summary>
        /// Obté o estableix el nom de la comarca.
        /// </summary>
        /// <value>El nom de la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? County { get; set; }

        /// <summary>
        /// Obté o estableix la població de la comarca.
        /// </summary>
        /// <value>La població de la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int Population { get; set; }

        /// <summary>
        /// Obté o estableix el nombre de connexions a la xarxa domèstica a la comarca.
        /// </summary>
        /// <value>El nombre de connexions a la xarxa domèstica a la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int HomeNetwork { get; set; }

        /// <summary>
        /// Obté o estableix el nombre d'activitats econòmiques a la comarca.
        /// </summary>
        /// <value>El nombre d'activitats econòmiques a la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int EconomicActivities { get; set; }

        /// <summary>
        /// Obté o estableix el consum total d'aigua a la comarca.
        /// </summary>
        /// <value>El consum total d'aigua a la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int TotalWaterConsumption { get; set; }

        /// <summary>
        /// Obté o estableix el consum domèstic d'aigua per càpita a la comarca.
        /// </summary>
        /// <value>El consum domèstic d'aigua per càpita a la comarca. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public decimal DomesticConsumptionPerCapita { get; set; }
    }
}