using System;
using System.Collections.Generic;
using System.Linq;
using FitnessApp.Logic.Interfaces;
using FitnessApp.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccessLayer.EF
{
    /// <summary>
    /// Реализует CRUD-операции с помощью Entity Framework.
    /// </summary>
    public class EntityRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly DBContext context;

        /// <summary>
        /// Создаёт репозиторий для работы с Entity Framework.
        /// </summary>
        public EntityRepository()
        {
            context = new DBContext();
        }

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Возвращает все сущности.
        /// </summary>
        public IEnumerable<T> ReadAll()
        {
            return context.Set<T>().ToList();
        }

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        public T ReadById(int id)
        {
            return context.Set<T>().FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }
    }
}