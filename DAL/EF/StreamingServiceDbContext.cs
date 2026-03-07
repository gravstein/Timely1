using DAL.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.EF
{
    // Это сердце доступа к данным. Мост между объектами в C# и таблицами в БД

    public class StreamingServiceDbContext : IdentityDbContext<AppUser, AppRole, string> 
        // наследуемся из класса который автоматически добавляет в базу таблицы для пользователей, ролей и тд
        // <КлассПользователя, КлассРоли, ТипIdДляНих>
    {
        public DbSet<Guitar> Guitars { get; set; } // DbSet создают таблицу в БД на основе модели 
        public DbSet<Brand> Brands { get; set; } // также DbSet позволяет обращаться к данным как к объектам C# 
        public DbSet<Category> Categories { get; set; } // эти обращения EF Core превратит в SQL запросы

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }

        public StreamingServiceDbContext(DbContextOptions<StreamingServiceDbContext> options) : base(options) { } 
        // коструктор который принимает настройки БД которые задаются в Programm.cs

        protected override void OnModelCreating(ModelBuilder builder) // метод инструкции для EF которая задаёт структуру БД
        {
            base.OnModelCreating(builder);

            var applicationContextAssembly = typeof(StreamingServiceDbContext).Assembly; // берём нашу сборку

            builder.ApplyConfigurationsFromAssembly(applicationContextAssembly); // сканируем её и находим Config-и и применяем их
            //builder.ApplyConfiguration(new RoleConfig()); // зачем то отдельно вручную применяем конфиг для роли?
        }
    }
}