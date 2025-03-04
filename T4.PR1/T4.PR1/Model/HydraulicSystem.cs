namespace T4.PR1.Model
{
    public class HydraulicSystem : AEnergySystem
    {
        public HydraulicSystem(decimal ratio) : base(ratio) { }

        public override decimal CalculateEnergy(decimal waterFlow)
        {
            if (waterFlow < 0)
                throw new ArgumentException("El cabal d'aigua ha de ser positiu.");

            return waterFlow * 9.8m * Ratio;
        }
    }
}
