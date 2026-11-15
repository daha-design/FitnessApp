using System;
using System.Collections.Generic;

namespace FitnessApp.Logic.Models
{
    /// <summary>
    /// Представляет тренировку пользователя.
    /// </summary>
    public class Training
    {
        /// <summary>
        /// Уникальный идентификатор тренировки.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название тренировки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата проведения тренировки.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Продолжительность тренировки в минутах.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Список упражнений данной тренировки.
        /// </summary>
        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Создаёт новую тренировку.
        /// </summary>
        public Training(int id, string name,
                        DateTime date, int duration)
        {
            Id = id;
            Name = name;
            Date = date;
            Duration = duration;
            Exercises = new List<Exercise>();
        }
    }
}
