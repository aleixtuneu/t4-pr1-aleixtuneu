namespace T4.PR1.Model
{
    /// <summary>
    /// Representa un sistema de generació d'energia eòlica.
    /// </summary>
    public class WindSystem : AEnergySystem
    {
        /// <summary>
        /// Constructor per la classe WindSystem.
        /// </summary>
        /// <param name="ratio">El rati del sistema eòlic. Ha d'estar en el rang (0,3].</param>
        public WindSystem(decimal ratio) : base(ratio) { }

        /// <summary>
        /// Calcula l'energia generada per un sistema eòlic.
        /// </summary>
        /// <param name="windSpeed">La velocitat del vent en metres per segon.</param>
        /// <returns>L'energia generada pel sistema eòlic.</returns>
        /// <exception cref="System.ArgumentException">Es llença si la velocitat del vent és negativa.</exception>
        public override decimal CalculateEnergy(decimal windSpeed)
        {
            if (windSpeed < 0)
                throw new ArgumentException("La velocitat del vent ha de ser positiva.");

            return (decimal)Math.Pow((double)windSpeed, 3) * Ratio;
        }
    }
}
