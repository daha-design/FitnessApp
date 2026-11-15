using System;
using System.Linq;
using FitnessApp.Logic.Models;
using FitnessApp.Logic.Services;

namespace FitnessApp.ConsoleApp
{
    /// <summary>
    /// Основной класс консольного приложения.
    /// </summary>
    internal class Program
    {
        private static readonly TrainingService _service = new TrainingService();

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                string choice = Console.ReadLine() ?? string.Empty;

                try
                {
                    switch (choice)
                    {
                        case "1": AddTraining(); break;
                        case "2": ShowTrainings(); break;
                        case "3": UpdateTraining(); break;
                        case "4": DeleteTraining(); break;
                        case "5": AddExercise(); break;
                        case "6": ShowTotalDuration(); break;
                        case "7": ShowTotalSets(); break;
                        case "0": return;
                        default: Console.WriteLine("Неверный выбор!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n===== FITNESS APP =====");
            Console.WriteLine("1. Добавить тренировку");
            Console.WriteLine("2. Показать тренировки");
            Console.WriteLine("3. Изменить тренировку");
            Console.WriteLine("4. Удалить тренировку");
            Console.WriteLine("5. Добавить упражнение");
            Console.WriteLine("6. Общая продолжительность");
            Console.WriteLine("7. Общее количество подходов");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите действие: ");
        }

        private static void AddTraining()
        {
            int id = ReadInt("ID: ");
            string name = ReadString("Название: ");
            int duration = ReadInt("Продолжительность (мин): ");

            Training training = new Training(id, name, DateTime.Now, duration);
            _service.AddTraining(training);

            Console.WriteLine("Тренировка добавлена!");
        }

        private static void ShowTrainings()
        {
            var trainings = _service.GetAllTrainings();

            if (trainings == null || !trainings.Any())
            {
                Console.WriteLine("Тренировок пока нет.");
                return;
            }

            foreach (Training t in trainings)
            {
                Console.WriteLine(
                    $"\nID: {t.Id} | Название: {t.Name} | " +
                    $"Дата: {t.Date.ToShortDateString()} | " +
                    $"Длительность: {t.Duration} мин");

                foreach (Exercise e in t.Exercises)
                {
                    Console.WriteLine(
                        $"  Упражнение: {e.Name}, " +
                        $"подходы: {e.Sets}, " +
                        $"повторения: {e.Repetitions}, " +
                        $"вес: {e.Weight} кг");
                }
            }
        }

        private static void UpdateTraining()
        {
            int updateId = ReadInt("ID тренировки: ");
            string newName = ReadString("Новое название: ");
            int newDuration = ReadInt("Новая продолжительность: ");

            bool updated = _service.UpdateTraining(updateId, newName, DateTime.Now, newDuration);

            Console.WriteLine(updated
                ? "Тренировка изменена!"
                : "Тренировка с таким ID не найдена.");
        }

        private static void DeleteTraining()
        {
            int deleteId = ReadInt("ID тренировки: ");

            bool deleted = _service.DeleteTraining(deleteId);

            Console.WriteLine(deleted
                ? "Тренировка удалена!"
                : "Тренировка с таким ID не найдена.");
        }

        private static void AddExercise()
        {
            int trainingId = ReadInt("ID тренировки: ");
            string exerciseName = ReadString("Название упражнения: ");
            int sets = ReadInt("Количество подходов: ");
            int repetitions = ReadInt("Количество повторений: ");
            double weight = ReadDouble("Вес (кг): ");

            Exercise exercise = new Exercise(
                exerciseName, sets, repetitions, weight);

            bool added = _service.AddExercise(trainingId, exercise);

            Console.WriteLine(added
                ? "Упражнение добавлено!"
                : "Тренировка с таким ID не найдена.");
        }

        private static void ShowTotalDuration()
        {
            Console.WriteLine(
                "Общая продолжительность: " +
                _service.GetTotalDuration() + " минут");
        }

        private static void ShowTotalSets()
        {
            Console.WriteLine(
                "Общее количество подходов: " +
                _service.GetTotalSets());
        }

        // ---------- Вспомогательные методы ввода ----------

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Ошибка: введите целое число.");
            }
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                Console.WriteLine("Ошибка: введите число.");
            }
        }

        private static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
                Console.WriteLine("Поле не может быть пустым.");
            }
        }
    }
}