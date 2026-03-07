using Mapster;
using Models.DTO;
using Models.Entities;

namespace Timely1.Extensions
{
    // настройки маппера
    public static class MapsterExtention
    {
        public static void InitMapping(this IApplicationBuilder app)
        {
            app.UserMapping();
        }

        private static void UserMapping(this IApplicationBuilder app)
        {
            // класс для настройки маппинга <ВоЧто, Откуда>
            TypeAdapterConfig<RegistrationDTO, AppUser>.NewConfig()
                .Map(dest => dest.UserName, src => src.Email) // делаем email нашим username
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => src.Password);
        }
    }
}
