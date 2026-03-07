using Abstraction.Interfaces.DataSourse;
using DAL.DataSource;

namespace Timely1.Extensions
{
    // вспомогательный класс для подключение сервисов для работы с данными получаемыми с БД
    public static class DataSourceServicesServiceExtension
    {
        public static void AddDataSourceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDataSourse<>), typeof(GenericDataSource<>)); // регистрируем источник данных
        }
    }
}
