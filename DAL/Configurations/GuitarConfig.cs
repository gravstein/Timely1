using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class GuitarConfig : IEntityTypeConfiguration<Guitar>
    {
        public void Configure(EntityTypeBuilder<Guitar> builder)
        {
            builder.Property(x => x.ModelName).HasMaxLength(100).IsRequired(); // наши constrains
            builder.Property(x => x.Price).HasPrecision(10,2).IsRequired(); // тут реализуется Fluent API

            builder.Property(x => x.TypeOfWood).HasMaxLength(100);

            //builder.Property(x => x.Brand).IsRequired(); если так написать то EF Core не знает что это отношение с другой моделью
            builder.HasOne(x => x.Brand) // у нашей гитары есть один бренд
                   .WithMany() // у бренда есть много гитар
                   .HasForeignKey("BrandId") // у нашей гитары должен быть внешний ключ бренда
                   .OnDelete(DeleteBehavior.Cascade) // если удалиться бренд то удаляться все гитары (по каскаду)
                   .IsRequired(); // обязательное поле

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
