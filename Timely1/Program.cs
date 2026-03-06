
using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using BLL.Services;
using DAL.DataSource;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Timely1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // настройка подключения к БД      
            builder.Services.AddDbContext<StreamingServiceDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericDataSourse<>), typeof(GenericDataSource<>)); // регистрируем источник данных

            // регистрируем сервисы
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IGuitarService, GuitarService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


// миграции
// dotnet ef migrations add InitialCreate --project DAL --startup-project Timely1
// dotnet ef database update --project DAL --startup-project Timely1