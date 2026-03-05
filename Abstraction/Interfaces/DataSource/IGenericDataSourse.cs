using System.Linq.Expressions;

namespace Abstraction.Interfaces.DataSourse
{
    // Универсальный слой доступа к данным. Может работать с разными типами сущностей
    // нужен чтобы определять тип данных которые мы забираем с бд
    // Должен реализоваться в слое DAL в классе GenericDataSourse где мы уже опишем работу с бд
    public interface IGenericDataSourse<T>
    {
        IQueryable<T> GetElements(Expression<Func<T, bool>>? filter = null);
        Task<T> AddAsync(T item);
        Task<T> RemoveAsync(T item);
        Task<T> UpdateBase(T item);
        Task SaveChangesAsync();
    }
}
