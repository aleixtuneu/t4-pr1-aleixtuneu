using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
    /// <summary>
    /// Representa una simulació energètica amb dades de sistema, entrada, rati i resultats.
    /// </summary>
    public class EnergySimulation
    {
        /// <summary>
        /// Obté o estableix la data de la simulació. Per defecte, és la data actual.
        /// </summary>
        /// <value>La data i hora en què es va realitzar la simulació.</value>
        public DateTime SimulationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Obté o estableix el tipus de sistema energètic utilitzat en la simulació.
        /// </summary>
        /// <value>El tipus de sistema energètic (Solar, Wind, Hydraulic). Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "El tipus de sistema és obligatori.")]
        public string SystemType { get; set; }

        /// <summary>
        /// Obté o estableix el valor d'entrada per a la simulació.
        /// </summary>
        /// <value>El valor d'entrada per al càlcul de l'energia. Aquest camp és obligatori i ha de ser major que 0.</value>
        [Required(ErrorMessage = "El valor d'entrada és obligatori.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El valor d'entrada ha de ser major que 0.")]
        public decimal InputValue { get; set; }

        /// <summary>
        /// Obté o estableix el rati utilitzat en la simulació.
        /// </summary>
        /// <value>El rati utilitzat per calcular l'energia. Aquest camp és obligatori i ha d'estar entre 0.01 i 3.</value>
        [Required(ErrorMessage = "El rati és obligatori.")]
        [Range(0.01, 3, ErrorMessage = "El rati ha d'estar entre 0.01 i 3.")]
        public decimal Ratio { get; set; }

        /// <summary>
        /// Obté o estableix l'energia generada per la simulació.
        /// </summary>
        /// <value>L'energia generada calculada pel sistema. Aquest camp és obligatori.</value>
        [Required(ErrorMessage = "L'energia generada és obligatòria.")]
        public decimal EnergyGenerated { get; set; }

        /// <summary>
        /// Obté o estableix el cost per kWh.
        /// </summary>
        /// <value>El cost per kWh. Aquest camp és obligatori i ha de ser positiu.</value>
        [Required(ErrorMessage = "El cost per kWh és obligatori.")]
        [Range(0, double.MaxValue, ErrorMessage = "El cost per kWh ha de ser positiu.")]
        public decimal CostPerKWh { get; set; }

        /// <summary>
        /// Obté o estableix el preu per kWh.
        /// </summary>
        /// <value>El preu per kWh. Aquest camp és obligatori i ha de ser positiu.</value>
        [Required(ErrorMessage = "El preu per kWh és obligatori.")]
        [Range(0, double.MaxValue, ErrorMessage = "El preu per kWh ha de ser positiu.")]
        public decimal PricePerKWh { get; set; }

        /// <summary>
        /// Obté el cost total calculat (EnergyGenerated * CostPerKWh).
        /// </summary>
        /// <value>El cost total de l'energia generada.</value>
        public decimal TotalCost => EnergyGenerated * CostPerKWh;

        /// <summary>
        /// Obté el preu total calculat (EnergyGenerated * PricePerKWh).
        /// </summary>
        /// <value>El preu total de l'energia generada.</value>
        public decimal TotalPrice => EnergyGenerated * PricePerKWh;

        /// <summary>
        /// Constructor per defecte de la classe EnergySimulation.
        /// </summary>
        public EnergySimulation() { }

        /// <summary>
        /// Constructor amb paràmetres per la classe EnergySimulation.
        /// </summary>
        /// <param name="systemType">El tipus de sistema energètic (Solar, Wind, Hydraulic).</param>
        /// <param name="inputValue">El valor d'entrada per al càlcul de l'energia.</param>
        /// <param name="ratio">El rati utilitzat per calcular l'energia.</param>
        /// <param name="costPerKWh">El cost per kWh.</param>
        /// <param name="pricePerKWh">El preu per kWh.</param>
        /// <exception cref="System.ArgumentException">Es llença si el tipus de sistema no és vàlid.</exception>
        public EnergySimulation(string systemType, decimal inputValue, decimal ratio, decimal costPerKWh, decimal pricePerKWh)
        {
            SystemType = systemType;
            InputValue = inputValue;
            Ratio = ratio;
            CostPerKWh = costPerKWh;
            PricePerKWh = pricePerKWh;

            AEnergySystem system = systemType switch
            {
                "Solar" => new SolarSystem(ratio),
                "Wind" => new WindSystem(ratio),
                "Hydraulic" => new HydraulicSystem(ratio),
                _ => throw new ArgumentException("Tipus de sistema invàlid.")
            };

            EnergyGenerated = system.CalculateEnergy(inputValue);
        }

        /// <summary>
        /// Retorna una representació en string de la simulació energètica.
        /// </summary>
        /// <returns>Una string amb les dades de la simulació separades per comes.</returns>
        public override string ToString()
        {
            return $"{SimulationDate},{SystemType},{InputValue},{Ratio},{EnergyGenerated},{CostPerKWh},{PricePerKWh},{TotalCost},{TotalPrice}";
        }
    }
}
