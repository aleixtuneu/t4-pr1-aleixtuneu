namespace T4.PR1.Model
{
    public class SolarSystem : AEnergySystem
    {
        public SolarSystem(decimal ratio) : base(ratio) { }

        public override decimal CalculateEnergy(decimal hoursOfSun)
        {
            if (hoursOfSun < 0)
                throw new ArgumentException("Les hores de sol han de ser positives.");

            return hoursOfSun * Ratio;
        }
    }
}
