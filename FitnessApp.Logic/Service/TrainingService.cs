using System;
using System.Collections.Generic;
using System.Linq;
using FitnessApp.Logic.Models;

namespace FitnessApp.Logic.Services
{
    /// <summary>
    /// Содержит бизнес-логику управления тренировками.
    /// </summary>
    public class TrainingService
    {
        /// <summary>
        /// Список всех тренировок.
        /// </summary>
        private readonly List<Training> trainings =
            new List<Training>();

        /// <summary>
        /// Добавляет новую тренировку.
        /// </summary>
        public void AddTraining(Training training)
        {
            trainings.Add(training);
        }

        /// <summary>
        /// Удаляет тренировку по идентификатору.
        /// </summary>
        public void DeleteTraining(int id)
        {
            Training training = trainings
                .FirstOrDefault(t => t.Id == id);

            if (training != null)
            {
                trainings.Remove(training);
            }
        }

        /// <summary>
        /// Возвращает список всех тренировок.
        /// </summary>
        public List<Training> GetAllTrainings()
        {
            return trainings;
        }

        /// <summary>
        /// Изменяет данные существующей тренировки.
        /// </summary>
        public void UpdateTraining(
            int id, string name,
            DateTime date, int duration)
        {
            Training training = trainings
                .FirstOrDefault(t => t.Id == id);

            if (training != null)
            {
                training.Name = name;
                training.Date = date;
                training.Duration = duration;
            }
        }

        /// <summary>
        /// Добавляет упражнение в указанную тренировку.
        /// </summary>
        public void AddExercise(int trainingId, Exercise exercise)
        {
            Training training = trainings
                .FirstOrDefault(t => t.Id == trainingId);

            if (training != null)
            {
                training.Exercises.Add(exercise);
            }
        }

        /// <summary>
        /// Подсчитывает общую продолжительность всех тренировок.
        /// </summary>
        public int GetTotalDuration()
        {
            return trainings.Sum(t => t.Duration);
        }

        /// <summary>
        /// Подсчитывает общее количество подходов.
        /// </summary>
        public int GetTotalSets()
        {
            return trainings.Sum(t =>
                t.Exercises.Sum(e => e.Sets));
        }
    }
}