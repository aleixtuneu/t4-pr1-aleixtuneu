namespace T4.PR1.Model
{
    public class WindSystem : AEnergySystem
    {
        public WindSystem(decimal ratio) : base(ratio) { }

        public override decimal CalculateEnergy(decimal windSpeed)
        {
            if (windSpeed < 0)
                throw new ArgumentException("La velocitat del vent ha de ser positiva.");

            return (decimal)Math.Pow((double)windSpeed, 3) * Ratio;
        }
    }
}
