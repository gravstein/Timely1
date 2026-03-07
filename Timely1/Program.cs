
using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using BLL.Services;
using DAL.DataSource;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Timely1.Extensions;



var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration; // config нашей сборки

// подключение к БД      
builder.Services.AddEntityFramework(config);

// регистрируем сервисы для работы с данными с БД
builder.Services.AddDataSourceServices();
// регистрируем сервисы
builder.Services.AddBllServices();

// настраиваем систему пользователей
builder.Services.AddIdentityServices();
// устанавливаем проверку по JWT
builder.Services.AddJwtAuthentication(config);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => // настройки сваггера
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {   // наша кнопка Authorize
        Description = "Введите JWT токен в формате: Bearer {token}", // подсказка
        Name = "Authorization", // имя заголовка в http запросе
        In = Microsoft.OpenApi.Models.ParameterLocation.Header, // пишем в заголовке запроса
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey, // тип защиты
        Scheme = "Bearer" // название стандарта
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {   // требование по безопасности авторизации   
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {   // используем настройки из метода AddSecurityDefinition
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2", // указываем протокол для авторизации OAuth2
                Name = "Bearer", // тип исплоьзуемой схемы
                In = Microsoft.OpenApi.Models.ParameterLocation.Header // указывает что данные должны передаваться в заголовке запроса
            },
            new List<string>() // массив областей доступа
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// применяем настройки маппера
app.InitMapping();

app.UseHttpsRedirection();

app.UseAuthentication(); // добавляем аутентификацию 
app.UseAuthorization();

app.MapControllers();

app.Run();


// миграции
// dotnet ef migrations add InitialCreate --project DAL --startup-project Timely1
// dotnet ef database update --project DAL --startup-project Timely1