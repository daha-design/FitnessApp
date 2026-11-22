using Microsoft.EntityFrameworkCore;
using FitnessApp.Logic.Models;

namespace FitnessApp.DataAccessLayer.EF
{
    /// <summary>
    /// Контекст базы данных Entity Framework.
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// Таблица тренировок.
        /// </summary>
        public DbSet<Training> Trainings { get; set; }

        /// <summary>
        /// Настраивает подключение к базе данных.
        /// </summary>
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;
                      Database=FitnessDatabase;
                      Trusted_Connection=True;
                      TrustServerCertificate=True;");
            }
        }

        /// <summary>
        /// Настраивает отображение сущностей в базе данных.
        /// </summary>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>()
                .HasKey(t => t.ID);

            // Упражнения пока не сохраняются в БД.
            modelBuilder.Entity<Training>()
                .Ignore(t => t.Exercises);
        }
    }
}
