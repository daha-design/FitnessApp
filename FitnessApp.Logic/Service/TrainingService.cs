using System;
using System.Collections.Generic;
using System.Linq;
using FitnessApp.Logic.Interfaces;
using FitnessApp.Logic.Models;

namespace FitnessApp.Logic.Services
{
    public class TrainingService
    {
        private readonly IRepository<Training> _repository;

        public TrainingService(IRepository<Training> repository)
        {
            _repository = repository;
        }

        public void AddTraining(Training training)
        {
            _repository.Add(training);
        }

        // ИСПРАВЛЕНО: теперь возвращает bool
        public bool UpdateTraining(int id, string name, DateTime date, int duration)
        {
            var training = _repository.ReadById(id);
            if (training != null)
            {
                training.Name = name;
                training.Date = date;
                training.Duration = duration;
                _repository.Update(training);
                return true; // Успешно обновлено
            }
            return false; // Тренировка не найдена
        }

        // ИСПРАВЛЕНО: теперь возвращает bool
        public bool DeleteTraining(int id)
        {
            var training = _repository.ReadById(id);
            if (training != null)
            {
                _repository.Delete(training);
                return true; // Успешно удалено
            }
            return false; // Тренировка не найдена
        }

        public IEnumerable<Training> GetAllTrainings()
        {
            return _repository.ReadAll();
        }

        public bool AddExercise(int trainingId, Exercise exercise)
        {
            var training = _repository.ReadById(trainingId);
            if (training == null) return false;

            // Внимание: если Exercises игнорируется в БД, они добавятся только в память.
            training.Exercises.Add(exercise);

            _repository.Update(training); // Пытаемся обновить
            return true;
        }

        public int GetTotalDuration()
        {
            return _repository.ReadAll().Sum(t => t.Duration);
        }

        public int GetTotalSets()
        {
            return _repository.ReadAll().Sum(t => t.Exercises.Sum(e => e.Sets));
        }
    }
}