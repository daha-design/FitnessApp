using System;
using System.Drawing;
using System.Windows.Forms;
using Ninject; // <--- ДОБАВЛЕНО для работы IoC-контейнера
using FitnessApp.Logic.Models;
using FitnessApp.Logic.Services;

namespace FitnessApp.WinForms
{
    public partial class Form1 : Form
    {
        // ИЗМЕНЕНО: Теперь мы не создаем репозиторий напрямую. 
        // Мы объявляем сервис, а Ninject сам его соберет.
        private readonly TrainingService service;

        private TextBox txtId = new TextBox();
        private TextBox txtName = new TextBox();
        private TextBox txtDuration = new TextBox();
        private TextBox txtExercise = new TextBox();
        private TextBox txtSets = new TextBox();
        private TextBox txtRepetitions = new TextBox();
        private TextBox txtWeight = new TextBox();

        private ListBox lstTrainings = new ListBox();

        public Form1()
        {
            // 1. Создаем ядро Ninject с нашим модулем конфигурации
            // (Убедитесь, что вы создали файл SimpleConfigModule.cs в проекте WinForms)
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // 2. Достаем готовый сервис из контейнера
            service = ninjectKernel.Get<TrainingService>();

            Text = "Fitness App";
            Size = new Size(600, 650);
            StartPosition = FormStartPosition.CenterScreen;

            AutoScaleMode = AutoScaleMode.Font;

            CreateInterface();
        }

        private void CreateInterface()
        {
            int y = 20;

            AddLabel("ID тренировки:", 20, y);
            txtId = AddTextBox(180, y);

            y += 40;
            AddLabel("Название:", 20, y);
            txtName = AddTextBox(180, y);

            y += 40;
            AddLabel("Продолжительность:", 20, y);
            txtDuration = AddTextBox(180, y);

            y += 45;
            AddButton("Добавить тренировку", 20, y, AddTraining);
            AddButton("Изменить тренировку", 230, y, UpdateTraining);

            y += 45;
            AddButton("Удалить тренировку", 20, y, DeleteTraining);
            AddButton("Показать тренировки", 230, y, ShowTrainings);

            y += 55;
            AddLabel("Название упражнения:", 20, y);
            txtExercise = AddTextBox(180, y);

            y += 40;
            AddLabel("Подходы:", 20, y);
            txtSets = AddTextBox(180, y);

            y += 40;
            AddLabel("Повторения:", 20, y);
            txtRepetitions = AddTextBox(180, y);

            y += 40;
            AddLabel("Вес (кг):", 20, y);
            txtWeight = AddTextBox(180, y);

            y += 45;
            AddButton("Добавить упражнение", 20, y, AddExercise);

            y += 45;
            AddButton("Статистика", 20, y, ShowStatistics);

            lstTrainings.Location = new Point(20, y + 45);
            lstTrainings.Size = new Size(540, 150);

            Controls.Add(lstTrainings);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(160, 25);
            Controls.Add(label);
        }

        private TextBox AddTextBox(int x, int y)
        {
            TextBox box = new TextBox();
            box.Location = new Point(x, y);
            box.Size = new Size(200, 25);
            Controls.Add(box);
            return box;
        }

        private void AddButton(string text, int x, int y, Action action)
        {
            Button button = new Button();
            button.Text = text;
            button.Left = x;
            button.Top = y;
            button.Size = new Size(200, 30);

            button.Click += (sender, e) => action();
            Controls.Add(button);
        }

        private void AddTraining()
        {
            if (!int.TryParse(txtId.Text, out int id) ||
                !int.TryParse(txtDuration.Text, out int duration) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Проверьте введённые данные!");
                return;
            }

            Training training = new Training(id, txtName.Text, DateTime.Now, duration);
            service.AddTraining(training);

            MessageBox.Show("Тренировка добавлена!");
            ShowTrainings();
        }

        private void UpdateTraining()
        {
            if (!int.TryParse(txtId.Text, out int id) ||
                !int.TryParse(txtDuration.Text, out int duration) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Проверьте введённые данные!");
                return;
            }

            bool updated = service.UpdateTraining(id, txtName.Text, DateTime.Now, duration);

            if (updated)
            {
                MessageBox.Show("Данные обновлены!");
                ShowTrainings();
            }
            else
            {
                MessageBox.Show("Тренировка с таким ID не найдена!");
            }
        }

        private void DeleteTraining()
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Введите корректный ID!");
                return;
            }

            bool deleted = service.DeleteTraining(id);

            if (deleted)
            {
                MessageBox.Show("Тренировка удалена!");
                ShowTrainings();
            }
            else
            {
                MessageBox.Show("Тренировка с таким ID не найдена!");
            }
        }

        private void AddExercise()
        {
            if (!int.TryParse(txtId.Text, out int id) ||
                !int.TryParse(txtSets.Text, out int sets) ||
                !int.TryParse(txtRepetitions.Text, out int reps) ||
                !double.TryParse(txtWeight.Text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double weight) ||
                string.IsNullOrWhiteSpace(txtExercise.Text))
            {
                MessageBox.Show("Проверьте данные упражнения!");
                return;
            }

            Exercise exercise = new Exercise(txtExercise.Text, sets, reps, weight);

            bool added = service.AddExercise(id, exercise);

            if (added)
            {
                MessageBox.Show("Упражнение добавлено!");
                ShowTrainings();
            }
            else
            {
                MessageBox.Show("Тренировка с таким ID не найдена!");
            }
        }

        private void ShowTrainings()
        {
            lstTrainings.Items.Clear();

            foreach (Training t in service.GetAllTrainings())
            {
                lstTrainings.Items.Add($"ID: {t.ID} | {t.Name} | {t.Duration} мин.");

                foreach (Exercise e in t.Exercises)
                {
                    lstTrainings.Items.Add($"   {e.Name}: {e.Sets} подходов, {e.Repetitions} повторений, {e.Weight} кг");
                }
            }
        }

        private void ShowStatistics()
        {
            MessageBox.Show(
                "Общая продолжительность: " + service.GetTotalDuration() + " минут\n" +
                "Общее количество подходов: " + service.GetTotalSets(),
                "Статистика");
        }
    }
}