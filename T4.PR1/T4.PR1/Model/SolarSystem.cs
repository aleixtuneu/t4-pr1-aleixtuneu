namespace T4.PR1.Model
{
    /// <summary>
    /// Representa un sistema de generació d'energia solar.
    /// </summary>
    public class SolarSystem : AEnergySystem
    {
        /// <summary>
        /// Constructor per la classe SolarSystem.
        /// </summary>
        /// <param name="ratio">El rati del sistema solar. Ha d'estar en el rang (0,3].</param>
        public SolarSystem(decimal ratio) : base(ratio) { }

        /// <summary>
        /// Calcula l'energia generada per un sistema solar.
        /// </summary>
        /// <param name="hoursOfSun">Les hores de sol.</param>
        /// <returns>L'energia generada pel sistema solar.</returns>
        /// <exception cref="System.ArgumentException">Es llença si les hores de sol són negatives.</exception>
        public override decimal CalculateEnergy(decimal hoursOfSun)
        {
            if (hoursOfSun < 0)
                throw new ArgumentException("Les hores de sol han de ser positives.");

            return hoursOfSun * Ratio;
        }
    }
}