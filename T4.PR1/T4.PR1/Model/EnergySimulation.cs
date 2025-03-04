namespace T4.PR1.Model
{
    public class EnergySimulation
    {
        public DateTime SimulationDate { get; set; }
        public string SystemType { get; set; }
        public decimal InputValue { get; set; }
        public decimal Ratio { get; set; }
        public decimal EnergyGenerated { get; set; }
        public decimal CostPerKWh { get; set; }
        public decimal PricePerKWh { get; set; }
        public decimal TotalCost => EnergyGenerated * CostPerKWh;  // 🔒 Només lectura
        public decimal TotalPrice => EnergyGenerated * PricePerKWh;  // 🔒 Només lectura

        public EnergySimulation() { }

        public EnergySimulation(string systemType, decimal inputValue, decimal ratio, decimal costPerKWh, decimal pricePerKWh)
        {
            SimulationDate = DateTime.Now;
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
