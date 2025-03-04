namespace T4.PR1.Model
{
    public class EnergySimulation
    {
        public DateTime SimulationDate { get; private set; }
        public string SystemType { get; private set; }
        public decimal InputValue { get; private set; }
        public decimal Ratio { get; private set; }
        public decimal EnergyGenerated { get; private set; }
        public decimal CostPerKWh { get; private set; }
        public decimal PricePerKWh { get; private set; }
        public decimal TotalCost => EnergyGenerated * CostPerKWh;  // 🔒 Només lectura
        public decimal TotalPrice => EnergyGenerated * PricePerKWh;  // 🔒 Només lectura

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
