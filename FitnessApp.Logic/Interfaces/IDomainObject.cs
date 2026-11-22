namespace FitnessApp.Logic.Interfaces
{
    /// <summary>
    /// Определяет общий интерфейс доменной сущности.
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        int ID { get; set; }
    }
}
