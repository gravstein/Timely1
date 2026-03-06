using Abstraction.Interfaces.DataSourse;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataSource
{
    // универсальный класс чтобы брать данные из БД и работать с ними при помощи описанных методов
    // <T> - универсальность

    public class GenericDataSource<T> : IGenericDataSourse<T> where T : class 
        // наш класс может принимать в себя любые классы (мы в него будем передавать наши модели)
    {
        protected readonly StreamingServiceDbContext DbContext; // связь с базой
        public GenericDataSource(StreamingServiceDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbSet<T> Set => DbContext.Set<T>(); // метод который находит нужную таблицу в БД 

        public IQueryable<T> GetElements(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null) // позволяет передавать лямбда выражения для фильтрации
        {
            if (filter is not null) // если была передана фильтрация то
                return Set.Where(filter).AsNoTracking(); 
                // фильтруем данные и отдаём их в IQueryable (запрос который выполнится только когда мы будем итерировать через данные или ToList их)
            return Set.AsNoTracking(); // забираем все данные без слежки за ними от EF
        }

        public async Task<T> AddAsync(T item)
        {
            await Set.AddAsync(item);
            return item;
        }
        public Task<T> RemoveAsync(T item)
        {
            Set.Remove(item);
            return Task.FromResult(item);
        }
        public Task<T> UpdateBase(T item)
        {
            Set.Update(item);
            return Task.FromResult(item);
        }

        public async Task SaveChangesAsync() // сохраняем изменения в БД
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
