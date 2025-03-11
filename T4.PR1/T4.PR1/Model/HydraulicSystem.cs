namespace T4.PR1.Model
{
    /// <summary>
    /// Representa un sistema de generació d'energia hidràulica.
    /// </summary>
    public class HydraulicSystem : AEnergySystem
    {
        /// <summary>
        /// Constructor per la classe HydraulicSystem.
        /// </summary>
        /// <param name="ratio">El rati del sistema hidràulic. Ha d'estar en el rang (0,3].</param>
        public HydraulicSystem(decimal ratio) : base(ratio) { }

        /// <summary>
        /// Calcula l'energia generada per un sistema hidràulic.
        /// </summary>
        /// <param name="waterFlow">El cabal d'aigua en metres cúbics per segon.</param>
        /// <returns>L'energia generada pel sistema hidràulic.</returns>
        /// <exception cref="System.ArgumentException">Es llença si el cabal d'aigua és negatiu.</exception>
        public override decimal CalculateEnergy(decimal waterFlow)
        {
            if (waterFlow < 0)
                throw new ArgumentException("El cabal d'aigua ha de ser positiu.");

            return waterFlow * 9.8m * Ratio;
        }
    }
}
