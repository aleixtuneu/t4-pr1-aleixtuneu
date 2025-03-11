namespace T4.PR1.Model
{
    /// <summary>
    /// Classe abstracta base per a sistemes de generació d'energia.
    /// Defineix una propietat Ratio i un mètode abstracte per calcular l'energia generada.
    /// </summary>
    public abstract class AEnergySystem
    {
        /// <summary>
        /// Obté o estableix el rati del sistema energètic. Aquest valor influeix en el càlcul de l'energia generada.
        /// </summary>
        /// <value>El rati del sistema energètic. Ha d'estar entre 0.01 i 3.</value>
        public decimal Ratio { get; set; }

        /// <summary>
        /// Constructor per la classe abstracta AEnergySystem.
        /// </summary>
        /// <param name="ratio">El rati del sistema energètic. Ha d'estar en el rang (0,3].</param>
        /// <exception cref="System.ArgumentException">Es llença si el rati no està en el rang (0,3].</exception>
        protected AEnergySystem(decimal ratio)
        {
            if (ratio <= 0 || ratio > 3m)
                throw new ArgumentException("El rati ha d'estar en el rang (0,3].");

            Ratio = ratio;
        }

        /// <summary>
        /// Calcula l'energia generada pel sistema.
        /// </summary>
        /// <param name="inputValue">El valor d'entrada per al càlcul de l'energia. El significat depèn del tipus de sistema.</param>
        /// <returns>L'energia generada pel sistema, basada en el valor d'entrada i el rati.</returns>
        public abstract decimal CalculateEnergy(decimal inputValue);
    }
}