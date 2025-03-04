namespace T4.PR1.Model
{
    public abstract class AEnergySystem
    {
        public decimal Ratio { get; private set; }

        protected AEnergySystem(decimal ratio)
        {
            if (ratio <= 0 || ratio > 0.3m)
                throw new ArgumentException("El rati ha d'estar en el rang (0,3].");

            Ratio = ratio;
        }

        public abstract decimal CalculateEnergy(decimal inputValue);
    }
}
