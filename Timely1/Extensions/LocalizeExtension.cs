using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Timely1.Extensions
{
    public static class LocalizeExtension
    {
        public static void AddLocalizationConfigs(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory>(provider => // добавляем фабрику для локализатора чтобы правильно искать путь
                new ResourceManagerStringLocalizerFactory( // находим ресурсный файл для локализации
                    Options.Create(new LocalizationOptions { ResourcesPath = "" }), // создаём локализатор фабрикой и даём путь
                    provider.GetRequiredService<ILoggerFactory>()
                )
            );

            services.Configure<RequestLocalizationOptions>(options => // настройка наших локализаций
            {
                var supportedCultures = new[] { "ru-RU", "en-US" }; // языки
                options.SetDefaultCulture(supportedCultures[0]) // язык по дефолту
                        .AddSupportedCultures(supportedCultures)
                        .AddSupportedUICultures(supportedCultures);
            });
        }
    }
}
