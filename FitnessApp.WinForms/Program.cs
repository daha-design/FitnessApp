using System;
using System.Windows.Forms;

namespace FitnessApp.WinForms
{
    /// <summary>
    /// Основной класс запуска Windows Forms приложения.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}