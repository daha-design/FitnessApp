using System;
using System.Collections.Generic;
using FitnessApp.Logic.Interfaces;

namespace FitnessApp.Logic.Models
{
    /// <summary>
    /// Представляет тренировку пользователя.
    /// </summary>
    public class Training : IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор тренировки.
        /// </summary>
        public int ID { get; set; }

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
        /// Список упражнений тренировки.
        /// </summary>
        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Создаёт новую тренировку.
        /// </summary>
        public Training(int id, string name, DateTime date, int duration)
        {
            ID = id;
            Name = name;
            Date = date;
            Duration = duration;
            Exercises = new List<Exercise>();
        }

        /// <summary>
        /// Конструктор для Entity Framework.
        /// </summary>
        public Training()
        {
            Name = string.Empty;
            Exercises = new List<Exercise>();
        }
    }
}