using Microsoft.EntityFrameworkCore; // ORM
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    // создаём роли для пользователей в БД

    public class RoleConfig : IEntityTypeConfiguration<AppRole> // с помощью этого интерфейса мы настраиваем каждую таблицу отдельно
    {
        public void Configure(EntityTypeBuilder<AppRole> builder) // метод конфига который задаёт настройку нужной нам таблицы используя определённый билдер
        {
            builder.HasData( // Seed
                new AppRole
                {
                    Id = "78a6f634-9336-407b-99f5-467321234c32", // без этого не делаются миграции
                    Name = "User",
                    NormalizedName = "USER"
                },
                new AppRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd5125",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new AppRole
                {
                    Id = "3d2b274e-5c0e-446f-96af-123d56fd6434",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }
            );
        }
    }
}
