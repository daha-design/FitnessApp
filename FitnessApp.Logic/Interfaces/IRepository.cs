using System.Collections.Generic;

namespace FitnessApp.Logic.Interfaces
{
    /// <summary>
    /// Определяет основные операции CRUD для сущностей.
    /// </summary>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Возвращает все сущности.
        /// </summary>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        T ReadById(int id);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        void Update(T entity);
    }
}
