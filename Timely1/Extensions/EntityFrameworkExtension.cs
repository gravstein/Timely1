using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Timely1.Extensions
{
    // вспомогательный класс для подключения бд (Sqlite)
    public static class EntityFrameworkExtension
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StreamingServiceDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }

    }
}
