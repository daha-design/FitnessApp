using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using FitnessApp.Logic.Interfaces;
using FitnessApp.Logic.Models;

namespace FitnessApp.DataAccessLayer.Dapper
{
    /// <summary>
    /// Реализует CRUD-операции с помощью Dapper.
    /// </summary>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        // Строка подключения (такая же, как в DbContext)
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;
              Database=FitnessDatabase;
              Trusted_Connection=True;
              TrustServerCertificate=True;";

        public void Add(T entity)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"
                    INSERT INTO Trainings (ID, Name, Date, Duration) 
                    VALUES (@ID, @Name, @Date, @Duration)";

                db.Execute(sql, entity);
            }
        }

        public void Delete(T entity)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Trainings WHERE ID = @ID";
                db.Execute(sql, new { ID = entity.ID });
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Trainings";
                return db.Query<T>(sql).ToList();
            }
        }

        public T ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Trainings WHERE ID = @Id";
                return db.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        public void Update(T entity)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"
                    UPDATE Trainings 
                    SET Name = @Name, Date = @Date, Duration = @Duration 
                    WHERE ID = @ID";

                db.Execute(sql, entity);
            }
        }
    }
}