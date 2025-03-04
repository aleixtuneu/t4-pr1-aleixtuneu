using System.ComponentModel.DataAnnotations;

namespace T4.PR1.Model
{
    public class EnergySimulation
    {
        public DateTime SimulationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El tipus de sistema és obligatori.")]
        public string SystemType { get; set; }

        [Required(ErrorMessage = "El valor d'entrada és obligatori.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El valor d'entrada ha de ser major que 0.")]
        public decimal InputValue { get; set; }

        [Required(ErrorMessage = "El rati és obligatori.")]
        [Range(0.01, 3, ErrorMessage = "El rati ha d'estar entre 0.01 i 3.")]
        public decimal Ratio { get; set; }

        [Required(ErrorMessage = "L'energia generada és obligatòria.")]
        public decimal EnergyGenerated { get; set; }

        [Required(ErrorMessage = "El cost per kWh és obligatori.")]
        [Range(0, double.MaxValue, ErrorMessage = "El cost per kWh ha de ser positiu.")]
        public decimal CostPerKWh { get; set; }

        [Required(ErrorMessage = "El preu per kWh és obligatori.")]
        [Range(0, double.MaxValue, ErrorMessage = "El preu per kWh ha de ser positiu.")]
        public decimal PricePerKWh { get; set; }

        public decimal TotalCost => EnergyGenerated * CostPerKWh;
        public decimal TotalPrice => EnergyGenerated * PricePerKWh;

        public EnergySimulation() { }

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

        public override string ToString()
        {
            return $"{SimulationDate},{SystemType},{InputValue},{Ratio},{EnergyGenerated},{CostPerKWh},{PricePerKWh},{TotalCost},{TotalPrice}";
        }
    }
}
