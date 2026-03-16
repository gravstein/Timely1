using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Timely1.Extensions
{
    public static class LocalizeExtension
    {
        public static void AddLocalizationConfigs(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = ""); // указываем путь к ресурсным файлам для локализации. 

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
