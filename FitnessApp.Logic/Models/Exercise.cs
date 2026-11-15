namespace FitnessApp.Logic.Models
{
    /// <summary>
    /// Представляет упражнение в составе тренировки.
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Название упражнения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество подходов.
        /// </summary>
        public int Sets { get; set; }

        /// <summary>
        /// Количество повторений.
        /// </summary>
        public int Repetitions { get; set; }

        /// <summary>
        /// Рабочий вес в килограммах.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Создаёт новое упражнение.
        /// </summary>
        public Exercise(string name, int sets,
                        int repetitions, double weight)
        {
            Name = name;
            Sets = sets;
            Repetitions = repetitions;
            Weight = weight;
        }
    }
}
